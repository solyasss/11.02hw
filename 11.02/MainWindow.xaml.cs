using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace hw
{
    public partial class MainWindow : Window
    {
        private static Mutex mutex_file = new Mutex(false, "d93cb2ac-6605-4a02-8afb-c8630f6ea2c7", out bool createdNew);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void start_button_click(object sender, RoutedEventArgs e)
        {
            start_button.IsEnabled = false;
            Thread thread_1 = new Thread(thread_first);
            thread_1.Start();
        }

        private void thread_first()
        {
            Dispatcher.Invoke(() =>
            {
                status_1.Text = "thread 1 is generating random numbers";
                progress_1.Value = 0;
            });
            Random random = new Random();
            int count_numbers = 500;
            List<int> numbers = new List<int>();
            for (int i = 0; i < count_numbers; i++)
            {
                int num = random.Next(1, 1000);
                numbers.Add(num);
                int prog = (i + 1) * 100 / count_numbers;
                Dispatcher.Invoke(() => { progress_1.Value = prog; });
                Thread.Sleep(10);
            }
            mutex_file.WaitOne();
            Thread thread_2 = new Thread(thread_second);
            thread_2.Start();
            try
            {
                using (StreamWriter writer = new StreamWriter("numbers.dat"))
                {
                    foreach (int num in numbers)
                        writer.WriteLine(num);
                }
                Dispatcher.Invoke(() => { status_1.Text = $"thread 1 generated {count_numbers} numbers in file"; });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => { status_1.Text = "Error with thread 1: " + ex.Message; });
            }
            finally
            {
                mutex_file.ReleaseMutex();
            }
        }

        private void thread_second()
        {
            while (!File.Exists("numbers.dat"))
            {
                Thread.Sleep(50);
            }
            while (true)
            {
                int lineCount = 0;
                try { lineCount = File.ReadAllLines("numbers.dat").Length; } catch { }
                if (lineCount >= 500)
                    break;
                Thread.Sleep(50);
            }
            Dispatcher.Invoke(() =>
            {
                status_2.Text = "waiting to mutex";
                progress_2.Value = 0;
            });
            mutex_file.WaitOne();
            Dispatcher.Invoke(() =>
            {
                status_2.Text = "thread 2 is filtering prime numbers";
            });
            List<int> numbers = new List<int>();
            try
            {
                using (StreamReader sr = new StreamReader("numbers.dat"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int num))
                            numbers.Add(num);
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => { status_2.Text = "Error with reading: " + ex.Message; });
            }
            finally
            {
                mutex_file.ReleaseMutex();
            }
            List<int> primes = new List<int>();
            int total = numbers.Count;
            for (int i = 0; i < total; i++)
            {
                int num = numbers[i];
                if (prime_num(num)) primes.Add(num);
                int prog = (i + 1) * 100 / total;
                Dispatcher.Invoke(() => { progress_2.Value = prog; });
                Thread.Sleep(5);
            }
            mutex_file.WaitOne();
            Thread thread_3 = new Thread(thread_third);
            thread_3.Start();
            try
            {
                using (StreamWriter sw = new StreamWriter("primes_num.dat"))
                {
                    foreach (int num in primes) sw.WriteLine(num);
                }
                Dispatcher.Invoke(() => { status_2.Text = $"thread 2 found {primes.Count} prime numbers in file"; });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => { status_2.Text = "Error writing prime numbers: " + ex.Message; });
            }
            finally
            {
                mutex_file.ReleaseMutex();
            }
        }

        private void thread_third()
        {
            while (!File.Exists("primes_num.dat"))
            {
                Thread.Sleep(50);
            }
            while (true)
            {
                try
                {
                    if (new FileInfo("primes_num.dat").Length > 0)
                        break;
                }
                catch { }
                Thread.Sleep(50);
            }
            Dispatcher.Invoke(() =>
            {
                status_3.Text = "waiting to mutex";
                progress_3.Value = 0;
            });
            mutex_file.WaitOne();
            Dispatcher.Invoke(() =>
            {
                status_3.Text = "thread 3 is filtering primes ending with 7";
            });
            List<int> primes = new List<int>();
            try
            {
                using (StreamReader sr = new StreamReader("primes_num.dat"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int num))
                            primes.Add(num);
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => { status_3.Text = "Error: " + ex.Message; });
            }
            finally
            {
                mutex_file.ReleaseMutex();
            }
            List<int> primes7 = new List<int>();
            int total = primes.Count;
            for (int i = 0; i < total; i++)
            {
                int num = primes[i];
                if (num % 10 == 7) primes7.Add(num);
                int prog = (i + 1) * 100 / total;
                Dispatcher.Invoke(() => { progress_3.Value = prog; });
                Thread.Sleep(5);
            }
            mutex_file.WaitOne();
            try
            {
                using (StreamWriter sw = new StreamWriter("seven_numbers.dat"))
                {
                    foreach (int num in primes7) sw.WriteLine(num);
                }
                Dispatcher.Invoke(() => { status_3.Text = $"thread 3 found {primes7.Count} numbers ending with 7 in file"; });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => { status_3.Text = "Error with primes: " + ex.Message; });
            }
            finally
            {
                mutex_file.ReleaseMutex();
            }
        }

        private bool prime_num(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }
    }
}

using System;
using System.Threading;
using System.Windows;

namespace hw
{
    public partial class App : Application
    {
        public static Semaphore instance_semaphore;

        private void app_startup(object sender, StartupEventArgs e)
        {
            try
            {
                instance_semaphore = new Semaphore(3, 3, "synchronization_app_instance");
                if (!instance_semaphore.WaitOne(0))
                {
                    MessageBox.Show("Only three copies are allowed",
                        "Instance full limit", MessageBoxButton.OK, MessageBoxImage.Information);
                    Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Shutdown();
            }
        }

        private void app_exit(object sender, ExitEventArgs e)
        {
            if (instance_semaphore != null)
            {
                instance_semaphore.Release();
                instance_semaphore.Dispose();
            }
        }
    }
}
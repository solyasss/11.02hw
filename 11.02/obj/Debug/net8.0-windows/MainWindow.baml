   M S B A M L   `   `   `  ����	  11.02    hw.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  hw��5   	   $	��350��6   $	��500��6!   $	��500��6-   $	��500��6<   
�������.��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $��1��6   $��Center=�6"   $��Center=�6=   .+�5      �� -   5         ��Name$ status_1��5      $"��thread 1 is waiting to start��6*   $
��Bold"�5      $��14(�6*   $	��0,5q�68   � 5      -   5         �Name$ 
progress_1��5      $	��300��6.   $��20��6:   $	��0,5q�6F    � Minimum$ 0Q�6S    � Maximum$	 100Q�6_   �� 5      -   5        $ status_2��5      $"��thread 2 is waiting to start��6*   $
��Bold"�5      $��14(�6,   $	��0,5q�6:   � 5      -   5        $ 
progress_2��5      $	��300��6.   $��20��6:   $	��0,5q�6F   $ 0Q�6S   $	 100Q�6_   �� 5      -   5        $ status_3��5      $"��thread 3 is waiting to start��6*   $
��Bold"�5      $��14(�6+   $	��0,5q�69   � 5      -   5        $ 
progress_3��5      $	��300��6.   $��20��6:   $	��0,5q�6F   $ 0Q�6S   $	 100Q�6_   5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�5      �� -   5         ��Name$ start_button��5      $��Start��6+   
���ݠ��$	��120��5      $��30��6!   6-   5      5      5      
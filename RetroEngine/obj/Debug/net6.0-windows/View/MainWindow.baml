   M S B A M L   `   `   `  ����F  BRetroEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:RetroEngine.ViewModelsRetroEngine.ViewModels  5   	       RetroEngine.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:RetroEngine.ViewModels     5   	     � Icon$%  ../images/retro_engine_icon.png��5   	    � Title$ 
MainWindow��5	   	   $	��800��6   $
��1020��6)    � WindowStartupLocation$ CenterScreen=�66    � 
ResizeMode$ NoResize=�6[    � 	Resources 5
         buttonDesign �� 
TargetType �� Property �� Value��%a   &	        �� 5   
   " ��6%   .�5      ��  � ��$ Fonts/arcade.TTF )�6+   5      5       � DataContext 5      )   #RetroEngine.ViewModels.MainWindowVM  5   
   .��5      � ���#242424	 � ColumnDefinitions	 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   
 � RowDefinitions
 5   
   �� 5       6   �� 5!      6   �� 5"      6   �� 5#      6   �� 5$      6   �� 5%      6   �� 5&      6   �� 5'      6   �� 5(      6   �� 5)      6   �� 5*      6   .+�5,   
   �� $��Retro Engine 2024��6   $��0��6-   $��3��6=   $��30(�6Q   
�������$��Center=�6r   $��Center=�6�   6�   �� 5-   
   $'��!../images/retro_engine_poster.jpg��6   $��1��6;   $��0��6H   $��10��6X   $��2��6m   $
��1020��6~   $��Stretch=�6�   $
��Fill=�6�   6�   �� 5.   
   -   5        $��5��5.      $��1��6   $��2��6.   $��Create a new game��6B   ���#242424
�������
�������$��3q�5/   %   #��[  51      ��50      �� *create+��52      ��  �� ElementName$ 
imageState��54   5   �� 56   
   -   5        $��7��56      $��1��6   $��2��6/   $��Exit engine��6C   ���#242424
�������
�������$��3q�57   %   ��69   �� *exit+��58      �� $ 
imageState��5:   3   �� 5;   
   -   5         ��Name$ 
imageState��5;      $��5��6$   $��4��64   $��6��6A   $��Stretch=�6S   $��4��6q   6�   5?      5@      
   M S B A M L   `   `   `  ����E  AGameSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameSystem
GameSystem  5   	   $    GameSystem.CreateAccountWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GameSystem     5   	     � Title$  Create an account��5   	   $	��400��6#   $	��400��60    � WindowStartupLocation$ CenterScreen=�6<    � 
ResizeMode$ NoResize=�6a   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� -   5        $��txtCreateAccount��5      $��2��6,   $��2��6@   $��Center=�6R   $��Center=�6m   $��18(�6�   $��WrapWithOverflow=�6�   \ZIn order to become a member of this fantastic game, please enter the following information6  �� 5   
   -   5        $��lblUsername��5      $��0��6#   $��2��63   $��Right=�6@   $��Center=�6\   $��	Username:��6w   �� 5   
   -   5        $��txtUsername��5      $��1��6%   $��2��65   $
��Left=�6B   $��Center=�6]   $	��100��6x   �� 5   
   -   5        $��lblEmail��5      $��0��6    $��3��60   $��Right=�6=   $��Center=�6Y   $��Email:��6t   �� 5   
   -   5        $��txtEmail��5      $��1��6"   $��3��62   $
��Left=�6?   $��Center=�6Z   $	��100��6u   �� 5   
   -   5        $��lblPassword��5      $��0��6#   $��4��63   $��Right=�6@   $��Center=�6\   $��	Password:��6w   G� 5   
   -   5        $��pswPassword��5      $��1��6)   $��4��69   $
��Left=�6F   $��Center=�6a   $	��100��6|   �� 5   
   -   5        $��lblConfirmPassword��5      $��0��6*   $��5��6:   $��Right=�6G   $��Center=�6c   $��Re-enter Password:��6~   G� 5    
   -	   5        $��pswConfirmPassword��5       $��1��60   $��5��6@   $
��Left=�6M   $��Center=�6h   $	��100��6�   �� 5!   
   -
   5        $��btnCreateAccount��5!      $��2��6)   $��0��6:   $��6��6J   $��Right=�6W   $��Center=�6s   $��20q�6�   $��Create Account��6�   $��90��6�   6�   �� 5"   
   -   5        $��	btnCancel��5"      $��2��6"   $��1��63   $��6��6C   $
��Left=�6P   $��Center=�6k   $��20q�6�   $��Cancel��6�   $��90��6�   6�   5#      5$      
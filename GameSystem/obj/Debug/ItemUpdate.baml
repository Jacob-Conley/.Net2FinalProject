   M S B A M L   `   `   `  ����E  AGameSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameSystem
GameSystem  5   	       GameSystem.ItemUpdate   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GameSystem     5   	   -   5          � Title$  frmItemUpdate��5   	   $	��400��6   $	��600��6,    � WindowStartupLocation$ CenterScreen=�6O   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��200 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� -   5        $��	lblItemID��5      $��0��6!   $��0��61   $��Center=�6>   $��Right=�6Y   �� 5   
   -   5        $��lblName��5      $��0��6   $��1��6/   $��Center=�6<   $��Right=�6W   $
��Name��6s   �� 5   
   -   5        $��lblDescription��5      $��0��6&   $��2��66   $��Center=�6C   $��Right=�6^   $��Description��6z   �� 5   
   -   5        $��lblItemBoost1��5      $��0��6%   $��3��65   $��Center=�6B   $��Right=�6]   �� 5   
   -   5        $��lblItemBoost2��5      $��0��6%   $��4��65   $��Center=�6B   $��Right=�6]   �� 5   
   -   5        $��	txtItemID��5      $��1��6#   $��0��63   $��Center=�6@   $
��Left=�6[   $	��350��6w   �� 5   
   -   5        $��txtName��5      $��1��6!   $��1��61   $��Center=�6>   $
��Left=�6Y   $	��350��6u   �� 5   
   -	   5        $��txtDescription��5      $��1��6(   $��2��68   $
��Left=�6E   " �� VerticalScrollBarVisibility$
 Auto=�6`    �� TextWrapping$
 Wrap=�6�   $	��350��6�   �� 5   
   -
   5        $��txtItemBoost1��5      $��1��6'   $��3��67   $��Center=�6D   $
��Left=�6_   $	��350��6z   �� 5   
   -   5        $��txtItemBoost2��5      $��1��6'   $��4��67   $��Center=�6D   $
��Left=�6_   $	��350��6z   �� 5    
   -   5        $��btnSaveEdit��5       $
��Save��6$   $	��100��63   $��5��6?   $��5q�6L   $��Right=�6W   6s   �� 5!   
   -   5        $��	btnCancel��5!      $��Cancel��6"   $	��100��63   $��5��6?   $��1��6L   $
��Left=�6\   $��5q�6w   6�   5#      5$      
   M S B A M L   `   `   `  ����E  AGameSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameSystem
GameSystem  5   	   $    GameSystem.InventoryItemUpdate   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GameSystem     5   	   -   5          � Title$  InventoryItemUpdate��5   	   $	��200��6%   $	��300��62    � WindowStartupLocation$ CenterScreen=�6U   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      $��20 �6   6*   �� 5      $��20 �6   6*   �� 5      6   .+�5   
   �� -   5        $��txtDeleteInfo��5      $��3��6'   $��0��6;   $��Center=�6H   $��Center=�6e   �� 5   
   $��0��6   $��1��6    $��2��6-   $��Right=�6>   $��Center=�6Z   $��Quantity��6u   �� 5   
   -   5        $��txtQuantity��5      $��1��6%   $��1��65   $��2��6B   $��Center=�6S    �� TextAlignment$ Center=�6n    �� 
IsReadOnly . 6�   �� 5   
   -   5        $��btnIncrease��5      $��2��6$   $��1��64   $
��Left=�6A   $��20��6\   $��^��6g   6s   �� 5   
   -   5        $��btnDecrease��5      $��2��6$   $��2��64   $
��Left=�6A   $��20��6\   $��v��6g   6s   �� 5   
   -   5        $��	btnCancel��5      $��0��6"   $��2��62   $��3��6F   $��Center=�6S   $��Center=�6p   $��Cancel��6�   6�   �� 5   
   -   5        $��	btnDelete��5      $��1��6"   $��2��62   $��3��6F   $��Center=�6S   $��Center=�6p   $��Delete��6�   6�   5      5      
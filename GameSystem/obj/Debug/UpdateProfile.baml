   M S B A M L   `   `   `  ����E  AGameSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameSystem
GameSystem  5   	       GameSystem.UpdateProfile   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GameSystem     5   	   -   5          � Title$  Update Profile��5   	   $	��300��6    $	��500��6-    � WindowStartupLocation$ CenterScreen=�6P   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��75 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� -   5        $��	txtHeader��5      $��0��6%   $��0��62   $��2��6B   $��Center=�6V   $��Center=�6s   $V��PUpdate profile information,
 and choose to become a game editor from this window��6�    �� TextAlignment$ Center=�6�   $��18(�6  $
��Wrap=�6  �� 5   
   $��1��6   $��Center=�6   $��Right=�68   $��	Username:��6T   �� 5   
   -   5        $��txtUsername��5       �� 
IsReadOnly . $��1��67   $��1��6D   $
��Left=�6T   $��Center=�6o   �� 5   
   $��2��6   $��Center=�6   $��Right=�68   $��Email:��6T   �� 5   
   -   5        $��txtEmail��5      $��2��6"   $��1��6/   $
��Left=�6?   $��Center=�6Z   �� 5   
   $��3��6   $��Center=�6   $��Right=�68   $��New Password:��6T   G� 5   
   -   5        $��pswNewPassword��5      $��3��6,   $��1��69   $
��Left=�6I   $��Center=�6d   $	��125��6   �� 5    
   $��4��6   $��Center=�6   $��Right=�68   $��Re-enter Password:��6T   G� 5!   
   -   5        $��pswConfirmPassword��5!      $��4��60   $��1��6=   $
��Left=�6M   $��Center=�6h   $	��125��6�   �� 5"   
   -   5        $��lblGameEditor��5"      $��5��6%   $��Center=�62   $��Right=�6M   $��Become a Game Editor?��6i   �� 5#   
   -   5        $��chkGameEditor��5#      $��5��6(   $��1��65   $��Center=�6E   $
��Left=�6`   $	��Yes��6{   6�   �� 5$   
   -	   5        $��lblEditorTest��5$      $��6��6%   $��Center=�62   $��Right=�6M   $ ��What is the first pokemon:��6i   �� 5%   
   -
   5        $��txtTestResponse��5%      $��1��6)   $��6��69   $��Center=�6F   $
��Left=�6a   $	��100��6|   �� 5&   
   -   5        $��	btnDelete��5&      $��7��6"   $��2��6/   $��Center=�6@   $��Center=�6[   $��Delete Account��6x   6�   �� 5'   
   -   5        $��btnSave��5'      $��7��6    $��2��6-   $��1��6>   $��Center=�6N   $��Center=�6i   $��Save Changes��6�   6�   5(      5)      
   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5          WpfApp2.Gate   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfApp2     5      -   5        $��Outil��5   %   .��5	      �� -   5          ��Name$  
OutilShape��5	      $��74��6!   $��74��6,   
������ .+�5   
   F� -   5         F�Name$
 path��5      $��3��6   
���   �
���   �$��22,22 22,22q�6i   $��30��6~    �� Stretch$
 Fill=�6�   $��30��6�    � ContextMenu 5      �� 5      -   5         ��Name$
 menu��5      
�������.@�5      �� -   5        $��Ajouter entrée��5   "   6;   �� 5      -   5        $��Supprimer entrée��5   "   6=   �� 5      -   5        $��Ajouter Label��5   "   6:   �� 5      -   5        $��	Supprimer��5   "   66   5      5      � 5   
   -	   5         �Name$ TopGate��5      $	��Top=�6!   $��Stretch=�6:    �� Left$ 22��6X   ��5      ��  �� ElementName$
 path�� �� Path$ ActualWidth�5      � 5    
   -
   5        $ 
BottomGate��5       $	��Top=�6#   $��Stretch=�6;   $ 20��6Z   ��5!      �� $
 path��$ ActualWidth�z�5"      V� 5#      .~�5$      �� 	 �� ScaleY$	 -1Q�6%   64   T� 5%      
 T� Y
 6)   �� $ 
OutilShape��$ ActualHeight�6c   5&      5+      � 5-   
   -   5        $ LeftGate��5-      $	��Top=�6"   $��Stretch=�6:   
 �� Top$ 22��6X   ��5.      �� $
 path��$ ActualHeight�z�5/      V� 50      .~�51      ��  �� Angle$	 -90Q�6&   66   T� 52      
 6)   �� $
 path��$ ActualHeight�6]   53      58      � 5:   
   -   5        $ 	RightGate��5:      $	��Top=�6#   $��Right=�6;   $ 22��5;   E   ��6   �� $
 path��$ ActualHeight�z�5=      V� 5>      .~�5?      �� $ 90Q�6&   65   T� 5@       T� X 6)   �� $ 
OutilShape��$ ActualWidth�6b   5A      5F      5G      5H      
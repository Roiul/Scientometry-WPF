   M S B A M L   `   `   `  ����F  BPSO_Proiect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PSO_ProiectPSO_Proiect  5          PSO_Proiect.addPubWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:PSO_Proiect     5      .��5
      � .+�5   
   �� ���	#FFA4A49F.��5      �� .+�5      �� $��Adăugare articol��6   $��
ExtraBlack"�5      $��0,15,0,0q�5      $��45(�5      $��Center=�5      �� 5      -   5          ��Name$  pubPanel��5      $��0,15,0,0q�5      $	��Top=�5      
�������.+�5      �� $��Detalii publicatie��6    $��17(�5       $��Black"�5       $��Center=�5       $��15,0,0,3q�5       �� 5       �� Orientation$ 
Horizontal=�6!   $��0,0,0,4q�5   !   .+�5      ��  �� TextAlignment$ Center=�6$   $
��Wrap=�5   $   $
��Nume��5   $   $	��139��5    $   $��21��5!   $   $��15(�5"   $   �� 5#      -   5         ��Name$ pubNameTextBox��5#   "    �� TextWrapping$
 Wrap=�5$   "   $	��599��5%   "   5&      �� 5'      $ 
Horizontal=�6!   $��0,0,0,4q�5(   !   .+�5)      �� $ Center=�6$   $
��Wrap=�5*   $   $	��WOS��5+   $   $	��139��5,   $   $��21��5-   $   $��15(�5.   $   �� 5/      -   5        $ editorTextBox��5/   "   $
 Wrap=�50   "   $	��599��51   "   52      �� 53      $ 
Horizontal=�6!   $��0,0,0,7q�54   !   .+�55      �� $ Center=�6$   $
��Wrap=�56   $   $	��Tip��57   $   $	��139��58   $   $��21��59   $   $��15(�5:   $   �� 5;      -   5         ��Name$ typeComboBox��5;   #   $	��599��5<   #   5=      5>      �� 5@      -   5        $  articolPanel��5@      $	��Top=�5A      ���	#FFA4A49F.+�5C      �� $��Articol��6    $��17(�5D       $��Black"�5E       $��Center=�5F       $��15,0,0,3q�5G       �� 5H      $ 
Horizontal=�6!   $��0,0,0,4q�5I   !   .+�5J      �� $ Center=�6$   $
��Wrap=�5K   $   $
��Nume��5L   $   $	��139��5M   $   $��21��60   $��15(�5N   $   �� 5O      -   5        $ nameTextBox��5O   "   $
 Wrap=�5P   "   $	��599��5Q   "   5R      �� 5S      $ 
Horizontal=�6!   $��0,0,0,4q�5T   !   .+�5U      �� $ Center=�6$   $
��Wrap=�5V   $   $	��WOS��5W   $   $	��139��5X   $   $��21��5Y   $   $��15(�5Z   $   �� 5[      -   5        $ 
wosTextBox��5[   "   $
 Wrap=�5\   "   $	��599��5]   "   5^      �� 5_      $ 
Horizontal=�6!   $��0,0,0,4q�5`   !   .+�5a      �� $ Center=�6$   $
��Wrap=�5b   $   $	��DOI��5c   $   $	��139��5d   $   $��21��5e   $   $��15(�5f   $   �� 5g      -   5        $ 
doiTextBox��5g   "   $
 Wrap=�5h   "   $	��599��5i   "   5j      �� 5k      $ 
Horizontal=�6!   $��0,0,0,4q�5l   !   .+�5m      �� $ Center=�6$   $
��Wrap=�5n   $   $��Factor impact��5o   $   $	��139��5p   $   $��21��5q   $   $��15(�5r   $   �� 5s      -	   5        $ impactFactorTextBox��5s   "   $
 Wrap=�5t   "   $	��599��5u   "   5v      �� 5w      $ 
Horizontal=�6!   $��0,0,0,4q�5x   !   .+�5y      �� $ Center=�6$   $
��Wrap=�5z   $   $��Mod prezentare��5{   $   $	��139��5|   $   $��21��5}   $   $��15(�5~   $   �� 5      -
   5        $ modeComboBox��5   #   $	��599��5�   #   5�      5�      �� 5�      -   5        $  authorPanel��5�      $��0,0,0,0q�5�      $	��Top=�5�      
�������.+�5�      �� $��Autori��6    $��17(�5�       $��Black"�5�       $��Center=�5�       $��15,0,0,3q�5�       �� 5�      $ 
Horizontal=�6!   $��0,0,0,4q�6:   .+�5�      �� $ Center=�6$   $
��Wrap=�5�   $   $��Autor��5�   $   $	��139��5�   $   $��21��5�   $   $��15(�5�   $   �� 5�      -   5        $ authorComboBox��5�   #   $	��599��5�   #   5�   #   5�      �� 5�      $ 
Horizontal=�6!   $��0,0,0,4q�5�   !   .+�5�      �� $ Center=�6$   $	��Top=�5�   $   $
��Wrap=�5�   $   $��Lista autori��5�   $   $	��139��5�   $   $��21��5�   $   $��15(�5�   $   &   System.Windows.Controls.DataGrid  5�      -   5          Name$ authorsDataGrid��5�   #   $	��150��5�   #   $	��599��5�   #      CanUserAddRows .   � ContextMenu 5�      �� 5�   "   .@�5�   &   m� -   5        	 m�Name$	 deleteRecord��5�   /   $��Delete��5�   /   5�   /   5�   #   
   Columns
 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�   "   ,  &System.Windows.Controls.DataGridColumn   Header$ 	Principal��69      CellTemplate 5�   &   �� 5�   *   .R�5�   .   ��  d� 	IsChecked 67   �� *
IsSelected+ �� UpdateSourceTrigger$ PropertyChanged=�5�   +   5�   #   0  *System.Windows.Controls.DataGridTextColumn  5�   "   -   5        	lname��$
 Nume��5�   5      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 5�   5      CanUserResize .     
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   5   �� *lName+5�   5     5�   "   -   5        	fname��$ Prenume��5�   5   $	 160 5�   5    .   .  5�   5   �� *fName+5�   5     5�   "   -   5        	UEFID��$ UEFID��5�   5   $ 70 5�   5    .   .  5�   5   �� *uefid+5�   5     5�   "   -   5        Link��$
 Link��5�   5   $	 240 5�   5    .   .  5�   5   �� *link+5�   5   5�      5�      5�      �� 5�      -   5        $  articolDetailsPanel��5�      $	��Top=�5�      ���	#FFA4A49F.+�5�      �� $��Detalii articol��6    $��17(�5�       $��Black"�5�       $��Center=�5�       $��15,0,0,3q�5�       �� 5�      $ 
Horizontal=�6!   $��0,0,0,4q�5�   !   .+�5�      �� $ Center=�6$   $
��Wrap=�5�   $   $
��Anul��5�   $   $	��139��5�   $   $��21��5�   $   $��15(�5�   $   �� 5�      -   5        $ yearTextBox��5�   "   $
 Wrap=�5�   "   $	��599��5�   "   5�      �� 5�      $ 
Horizontal=�6"   $��0,0,0,4q�5�   "   .+�5�      �� $ Center=�6$   $
��Wrap=�5�   $   $��Pagina��5�   $   $	��139��5�   $   $��21��5�   $   $��15(�5�   $   �� 5�      -   5        $ pageTextBox��5�   "   $
 Wrap=�5�   "   $	��599��5�   "   5�      �� 5�      $ 
Horizontal=�6"   $��0,0,0,4q�5�   "   .+�5�      �� $ Center=�6$   $
��Wrap=�5�   $   $��Volum��5�   $   $	��139��5�   $   $��21��5�   $   $��15(�5�   $   �� 5�      -   5        $ volumeTextBox��5�   "   $
 Wrap=�5�   "   $	��599��5�   "   5�      �� 5�      $ 
Horizontal=�6"   $��0,0,0,4q�5�   "   .+�5�      �� $ Center=�6$   $
��Wrap=�5   $   $��Numar��5  $   $	��139��5  $   $��21��5  $   $��15(�5  $   �� 5     -   5        $ numberTextBox��5  "   $
 Wrap=�5  "   $	��599��5  "   5     5	     �� 5
     $ 
Horizontal=�6   $��	0,10,0,10q�5     $��Center=�5     .+�5     �� -   5         ��Name$ backToMainButton��5     $��0,0,20,0q�5     $��Înapoi��5     $��Center=�5     $��27��5     $	��206��5     5     �� 5     -   5        $ addArticolButton��5     $��20,0,0,0q�5     $��Adaugă��5     $��Center=�5     $��27��5     $	��206��5     5     5     5     5     5     5     
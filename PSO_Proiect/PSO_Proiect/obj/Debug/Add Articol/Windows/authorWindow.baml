   M S B A M L   `   `   `  ����F  BPSO_Proiect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PSO_ProiectPSO_Proiect  5   	       PSO_Proiect.authorWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:PSO_Proiect     5   	     � Title$  authorWindow��5   	   $	��392��6   $	��306��6+   .��5	      � $��0,0,0,8q�6   .+�5
   
   �� $
��Left=�6   $��
111,-2,0,0q�6/   $
��Wrap=�6C   $��	Autor nou��6W   $	��Top=�6h   $��37��6�   $	��108��6�   $��30(�6�   �� 5   
   $
��Left=�6   $��	52,45,0,0q�6/   $
��Wrap=�6B   $
��Nume��6V   $	��Top=�6b   $��20��6z   $��43��6�   �� 5   
   $
��Left=�6   $��	52,65,0,0q�6/   $
��Wrap=�6B   $��Prenume��6V   $	��Top=�6e   $��20��6}   $��43��6�   �� 5   
   $
��Left=�6   $��	52,84,0,0q�6/   $
��Wrap=�6B   $
��Link��6V   $	��Top=�6b   $��20��6z   $��43��6�   �� 5   
   $
��Left=�6   $��
52,104,0,0q�6/   $
��Wrap=�6C   $��UEFID��6W   $	��Top=�6d   $��20��6|   $��43��6�   �� 5   
   $
��Left=�6   $��
52,124,0,0q�6/   $
��Wrap=�6C   $��Afiliere��6W   $	��Top=�6g   $��20��6   $��43��6�   �� 5   
   $
��Left=�6   $��
52,144,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��20��6o   $��70��6{   �� 6�   $�Lista afilierei��6�   �� 6�   �� 6�   6�   �� 5   
   -   5         ��Name$ lastNameBox��5      $
��Left=�6'   $��
135,45,0,0q�6B    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j   $	��153��6�   �� 5   
   -   5        $ firstNameBox��5      $
��Left=�6(   $��
135,64,0,0q�6C   $
 Wrap=�6W   $	��Top=�6k   $	��153��6�   �� 5   
   -   5        $ linkBox��5      $
��Left=�6#   $��
135,83,0,0q�6>   $
 Wrap=�6R   $	��Top=�6f   $	��153��6~   �� 5   
   -   5        $ uefidBox��5      $
��Left=�6$   $��135,102,0,0q�6?   $
 Wrap=�6T   $	��Top=�6h   $	��153��6�   �� 5   
   -   5         ��Name$ affiliationComboBox��5      $
��Left=�60   $��135,121,0,0q�6K   $	��Top=�6`   $	��153��6x   $��21��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ affiliationDataGrid��5      $��135,144,18,74q�60    � ContextMenu 5      �� 5      .@�5      m� -   5         m�Name$ deleteRecord��5      $��Delete��65   6E   5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        name��,  &System.Windows.Controls.DataGridColumn   Header$ Nume Afiliere��5   3   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 152 6c   
   CanUserResize
 .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   �� *Name+6o   5      �� 5    
   -	   5         ��Name$ 	addButton��5       $��Adauga��6$   $
��Left=�65   $��
61,326,0,0q�6P   $	��Top=�6d   $��79��6|   6�   �� 5!   
   -
   5        $ 
exitButton��5!      $��Renunta��6%   $
��Left=�67   $��160,326,0,0q�6R   $	��Top=�6g   $��79��6   6�   5#      5$      
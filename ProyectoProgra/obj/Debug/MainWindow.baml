   M S B A M L   `   `   `  ����  ProyectoProgra    ProyectoProgra.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  My App��5   	   $	��450��6   $
��1200��6%   .��5      � ���#adf7b6 � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ InventarioDataGrid��5   G   $��0��6   $��5q�6       AutoGenerateColumns .     SelectionMode$ Single=�6c      SelectionUnit$ FullRow=�6z      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+  5      $ Producto��6%    67   �� *
Producto+  5      $ Cantidad��6%    67   �� *
Cantidad+  5      $ Precio��6%    65   �� *Precio+  5      $ Descripción��6%    6:   �� *Descripcion+5      �� 5   
   $��1��6   	 �� Orientation$	 
Horizontal=�6"   $��5q�6;   ���#c7f6d4.+�5      �� -   5        
 ��Name$
 TxtID��5   I   $	��ID:��6   $��Center=�6#   $��5q�6>   6I   �� 5      -   5         ��Name$ 
TxtIDValue��5   -   $	��200��6   $��5q�6"   6-   �� 5      $��	Producto:��6   $��Center=�6)   $��5q�6D   �� 5      -   5        $ TxtProducto��5   -   $	��200��6   $��5q�6"   6-   �� 5      $��	Cantidad:��6   $��Center=�6)   $��5q�6D   �� 5      -   5        $ TxtCantidad��5   -   $	��200��6   $��5q�6"   6-   �� 5      $��Precio:��6   $��Center=�6'   $��5q�6B   �� 5      -   5        $ 	TxtPrecio��5   -   $	��200��6   $��5q�6"   6-   �� 5      $��Descripción:��6   $��Center=�6,   $��5q�6G   �� 5       -   5        $ TxtDescripcion��5    -   $	��200��6   $��5q�6"   6-   5!      �� 5#   
   $��2��6   $��Right=�6"   $	 
Horizontal=�6>   $��5q�6W   .+�5$      �� -   5         ��Name$ 
BtnAgregar��5$      $��Agregar��6)   $��5q�6;   ���#2196F3
�������6n   �� 5%      -	   5        $ BtnActualizar��5%      $��
Actualizar��6,   $��5q�6A   ���#4CAF50
�������6t   �� 5&      -
   5        $ BtnEliminar��5&      $��Eliminar��6*   $��5q�6=   ���#F44336
�������6p   5'      5(      5)      
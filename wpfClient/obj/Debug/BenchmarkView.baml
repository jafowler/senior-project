   M S B A M L   `   `   `  ����D  @wpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:wpfClient	wpfClient  5          wpfClient.BenchmarkView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:wpfClient     5      $��191.784��5      $��297.022��60   .��5      � $��0,0,-225,-191q�6   .+�5	   
   �� -   5          ��Name$  textBox��5	      $
��Left=�6#   $��23��6>   $��0,26,0,0q�6J    �� TextWrapping$
 Wrap=�6\   $	��Top=�6�   $	��120��6�   ��6p   �� *
UserTime+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .  6�   �� 5
   
   -   5         ��Name$ label��5
      $��Time (in seconds):��6   $
��Left=�6<   $	��Top=�6W   $	��120��6o   �� 5   
   -   5        $  	packetBox��5      $
��Left=�6%   $��23��6@   $��0,75,0,0q�6L   $
 Wrap=�6^   $	��Top=�6�   $	��120��6�   ��6r   �� *UserPacketSize+$ PropertyChanged=� .  6�   �� 5   
   -   5        $ packetLabel��5      $��Packet Size (in bytes):��6%   $
��Left=�6G   $��0,49,0,0q�6b   $	��Top=�6t   $	��129��6�   �� 5   
   -   5         ��Name$ slider��5       �� IsSnapToTickEnabled .  � Maximum$	 100Q�6<    �� TickPlacement$ BottomRight=�6J   	 �� TickFrequency$	 5Q�6�   $
��Left=�6�   $��	0,144,0,0q�6�   $	��Top=�6�   $	��120��6�   
 � Value
 6f   �� *SliderValue+6�   �� 5   
   -   5        $ label1��5      $��Read / Write Ratios��6    $
��Left=�6>   $��	0,118,0,0q�6Y   $	��Top=�6l    J� RenderTransformOrigin$ 0.544,-0.013&�6�   �� 5   
   -   5        $  textBox1��5      $
��Left=�6$   $��23��6?   $��125,144,0,0q�6K   $
 Wrap=�6`   $	��Top=�6�   $��48��6�   ��6t   �� *SliderValue+6�   5      5      
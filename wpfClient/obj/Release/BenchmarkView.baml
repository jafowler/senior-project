   M S B A M L   `   `   `  ���� D  @wpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:wpfClient	wpfClient      wpfClient.BenchmarkView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:wpfClient     $��191.784��$��297.022��.��� $��0,0,-225,-191q�.+��� -     ��Name$  textBox��$
��Left=�$��23��$��0,26,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120������ *
UserTime+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .  �� -    ��Name$ label��$��Time (in seconds):��$
��Left=�$	��Top=�$	��120���� -   $  	packetBox��$
��Left=�$��23��$��0,75,0,0q�$
 Wrap=�$	��Top=�$	��120������ *UserPacketSize+$ PropertyChanged=� .  �� -   $ packetLabel��$��Packet Size (in bytes):��$
��Left=�$��0,49,0,0q�$	��Top=�$	��129���� -    ��Name$ slider�� �� IsSnapToTickEnabled .  � Maximum$	 100Q� �� TickPlacement$ BottomRight=�	 �� TickFrequency$	 5Q�$
��Left=�$��	0,144,0,0q�$	��Top=�$	��120��
 � Value
 �� *SliderValue+�� -   $ label1��$��Read / Write Ratios��$
��Left=�$��	0,118,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.544,-0.013&��� -   $  textBox1��$
��Left=�$��23��$��125,144,0,0q�$
 Wrap=�$	��Top=�$��48������ *SliderValue+
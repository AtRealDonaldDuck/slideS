   M S B A M L   `   `   `  ���� N  JFlickrSlideshow 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FlickrSlideshow_1._0FlickrSlideshow_1._0  -    'FlickrSlideshow_1._0.CreatePresetWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:FlickrSlideshow_1._0     -     � Title$  Preset Window��$	��200��$	��320�� � 
ResizeMode$ NoResize=�.���  � ColumnDefinitions �� $��30 ��� $
��auto ��� $
��auto ��� $��* ��� $��30 � � RowDefinitions �� $��15 ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $��15 �.+��� $��Preset Name: ��$��2��$��1��$��Right=��� -    ��Name$ txtBoxPresetName��$��3��$��1���� $��Gallery ID: ��$��2��$��2��$��Right=�� $��3��$��2�� �� $��* ��� $
��auto �.+��� -   $ txtBoxGalleryId��$��0���� -    ��Name$ btnBrowseGalleries��$	��...��$��1��$��20���� $��Transition Time: ��$��2��$��3��$��Right=��� -   $ txtBoxTransitionTime��$��3��$��3���� $��Animation Time: ��$��2��$��4��$��Right=��� -   $ txtBoxAnimationTime��$��3��$��4��� $��3��$��5�� �� $��* ��� $
��auto ��� $
��auto � �� $��* ��� $
��auto �.+��� -   $ 	btnCancel��$��Cancel��$��20��$��50��$	��2,0q�$��Right=�$��Bottom=�$��1��$��1���� -   $ btnSave��$
��Save��$��20��$��50��$	��2,0q�$��Right=�$��Bottom=�$��2��$��1��
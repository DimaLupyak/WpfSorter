   M S B A M L   `   `   `  ����F  BWpfMvvmDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfSorter.ViewWpfSorter.View  5   	   M IWpfSorterViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullkHclr-namespace:WpfSorter.ViewModel.Converters;assembly=WpfSorterViewModelWpfSorter.ViewModel.Converters 5   	   U=clr-namespace:WpfSorter.ViewModel;assembly=WpfSorterViewModelWpfSorter.ViewModel 5   	   #    WpfSorter.View.MainWindowView   5   	   &localclr-namespace:WpfSorter.View  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _
convertersHclr-namespace:WpfSorter.ViewModel.Converters;assembly=WpfSorterViewModel     5   	   S	ViewModel=clr-namespace:WpfSorter.ViewModel;assembly=WpfSorterViewModel     5   	     � Title$  Array Sorter��5   	   $	��272��6   $	��401��6+    � WindowStyle$
 None=�5	   	    � AllowsTransparency .    ThemedWindowStyle#���   66    � Icon$/ )/WpfMvvmDemo;component/Images/Dots Up.png��6b    � 	Resources 5
      ?  9WpfSorter.ViewModel.Converters.SleepTimeToStringConverter  sleepTimeToStringConverter  DataTemplate_Sorter"   WpfSorter.View.SorterControl��%u   &	       &	        5   
   60   �� 5   
   .R�5        $��1,1,1,1q�66   ��6"   �� 66   5      5      .��5      ]� .+�5   
   o� $	��Top=�6     	MenuStyle#���  6$   .@�5      m� $
��File��6     TopMenuItemStyle#���  6%   .@�5      m� $
��Open��6     SubItemControlTemplate#���  6)   .@�5      m� $��	From File��6     SubSubItemControlTemplate#���  5       m� Command 5   2   �� *LoadArrayFromFileCommand+5      m� 5      $��From DataBase��6   #���  5       5   6   �� *LoadArrayFromDBCommand+5      5      �� 5      m� 5      $
��Exit��6    6)   �� *CloseWindowCommand+5      5       `� 5!   
   $	��Top=�6   $��Stretch=�6+     ToolBarTrayStyle#���  6I   .�5"      c� $
��Left=�6     ToolBarStyle#���  68   .@�5#      �� $��Change All Sorters State��6    	 SimpleButtonStyle#��� 	 5%   *   ��5$      �� *ChangeAllSortersStateCommand+{�5%      �� *ArrayIsLoaded+6*   �� 5&      o� 5'      .@�5(      m� $��
Sleep Time��6   .@�5)      ��  �� Orientation$ 
Horizontal=�6%   .+�5*      ��  � Maximum$
 1000Q�5+   	    �� TickFrequency$ 50Q�5-   	   	 � SmallChange$	 10Q�6   
 � ToolTip$
 
Sleep time��5.   	    �� TickPlacement$
 Both=�5/   	    � LargeChange$ 50Q�6   $��80��6/    � Value 5,   	   �� *SorterSleepTime+ �� Mode$ TwoWay=�5/   /   �� 50      ��6$   �� *SorterSleepTime+ �� 	Converter# [ 51      52      53      54      55      �� 56      $
��Auto=�6    
 ScrollViewerStyle#��� 
 6>   .��57      �� #��[ 6O   ��6   �� *SorterControlViewModels+6O   58      59      5:      
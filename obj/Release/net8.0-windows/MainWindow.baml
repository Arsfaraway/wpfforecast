   M S B A M L   `   `   `  ���� H  DWeathForecast, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WeathForecastWeathForecast      WeathForecast.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:WeathForecast       � Title$  
MainWindow��$	��650��$
��1000��
������  � WindowStyle$
 None=� � AllowsTransparency$
 True�� � WindowStartupLocation$ CenterScreen=�.���  � Cursor$  ��.+��� $	��650��$
��1000�����#F7C7B1$��Center=�$��Center=� �� CornerRadius$ 30�����#AD4A2B$��7q�� .+��� -    ��Name$ BorderCityImageFirst�����#AD4A2B$��7q�$ 15��$y�Hidden=�$��40,40,740,390q�._��� $ 15��$
}�True��$��-47,-47,-47,-47q�$	��210��$	��210����!�  j� Stretch$ UniformToFill=� !� Visual �� -   	 ��Name$	 PhotoCityXAML��$,��&Images/icons8-cityscape-at-dusk-96.png���� -   $ BorderCityImageSecond�����#AD4A2B$��7q�$ 15��$y�Hidden=�$��40,40,740,390q�._��� $ 15��$
}�True��$��-47,-47,-47,-47q�$	��210��$	��210����!� $ UniformToFill=� �� -   $	 PhotoCityXAMLSecond��$,��&Images/icons8-cityscape-at-dusk-96.png���� -   $ BorderCityImageThird�����#AD4A2B$��7q�$ 15��$y�Visible=�$��40,40,740,390q�._��� -   $	 PhotoCityXAMLThird��$,��&Images/icons8-cityscape-at-dusk-96.png��$	��300��$	��300��$��Center=�$��Center=�$��Uniform=�$  ��]� $��315,75,131,395q�.+��� 
 �� Orientation$
 
Horizontal=�$	��117��$	��Top=�$��0,0,0,10q�$  ��.+��� ���#AD4A2B$��2q�$ 10�����#F7C7B1$��	12,0,10,0q�$	��400��$��40��$
 Hand��._��� -    ��Name$ SearchTextBox��$	��370��$��30��$��9,0,10,0q�$��20(�$��Center=�$
��Left=� �� TextAlignment$ Center=�$��0q����#AD4A2B���#F7C7B1 �� 
CaretBrush �#AD4A2B$  ���� -   $&�� Images/icons8-flying-duck-50.png��$��70��$��70��$��Center=�$
��Left=�$��5,0,0,0q�$
 Hand���� -	   $#��Images/icons8-shutdown-96.png��$��50��$��50��$	��Top=�$��Right=�$��	0,40,40,0q�$
 Hand���� -
    ��Name$ TempXAML��$��30°C�����#AD4A2B$��40,298,693,209q�$	��150(�$��Bodoni MT Condensed)��� -   $ WindXAML��$��10 m/s�����#AD4A2B$��352,298,314,188q�$	��150(�$��Bodoni MT Condensed)��� -   $ 	HumidXAML��$	��40%�����#AD4A2B$��761,298,50,209q�$	��150(�$��Bodoni MT Condensed)��� -   $ TimeXAML��$��12:51�����#AD4A2B$��315,26,264,494q�$��80(�$��Bodoni MT Condensed)� �� TextAlignment$ Center=��� -   $ CityXAML��$��Новосибирск�����#AD4A2B$��286,188,228,366q�$��60(�$
��Bold"�$��Bodoni MT Condensed)�$ Center=��� -   $2��,Images/Indicators/icons8-temperature-100.png��$	��134��$	��124��$	��Top=�$��Right=�$��0,484,805,0q�$
 Hand���� -   $*��$Images/Indicators/icons8-wind-96.png��$	��134��$	��124��$	��Top=�$
��Left=�$��425,478,0,0q�$
 Hand���� -   $/��)Images/Indicators/icons8-moisture-100.png��$	��134��$	��124��$	��Top=�$
��Left=�$��786,478,0,0q�$
 Hand���� -   $	 Thunderstorm��$3��-Images/Weather/icons8-cloud-lightning-100.png��$	��134��$	��124��$	��Top=�$
��Left=�$��
711,26,0,0q�$  ��$y�Hidden=��� -   $	 Clouds��$)��#Images/Weather/icons8-cloud-100.png��$	��134��$	��124��$	��Top=�$
��Left=�$��
709,18,0,0q�$  ��$y�Hidden=��� -   $	 Clear��$'��!Images/Weather/icons8-sun-100.png��$	��134��$	��124��$	��Top=�$
��Left=�$��
709,20,0,0q�$  ��$y�Hidden=��� -   $
	 Rain��$(��"Images/Weather/icons8-rain-100.png��$	��134��$	��124��$	��Top=�$
��Left=�$��
727,22,0,0q�$  ��$y�Hidden=��� -   $
	 Snow��$(��"Images/Weather/icons8-snow-100.png��$	��134��$	��124��$	��Top=�$
��Left=�$��
705,22,0,0q�$  ��$y�Hidden=��� -   $ 
BorderXAML��$	��800��$	��400��$ 20��$��100 125q�$y�Hidden=����#AD4A2B$��7q����#F7C7B1._�� .+��� -   $ ErrorConnectionTextXAML�����#AD4A2B$��20,118,20,117q�$��60(�$ Center=�$��Center=�$F��@Проверьте подключение
к интернету!��$��Bodoni MT Condensed)�$y�Hidden=��� -   $ ErrorServerTextXAML�����#AD4A2B$��20,118,20,117q�$��45(�$ Center=�$��Center=�$a��[Запрос превысил время ожидания.
Попробуйте позже.��$��Bodoni MT Condensed)�$y�Hidden=��� -   $ ErrorAnotherTextXAML�����#AD4A2B$��20,118,20,117q�$��43(�$ Center=�$��Center=�$d��^Произошла непредвиденная ошибка.
Попробуйте снова.��$��Bodoni MT Condensed)�$y�Hidden=��� -   $	 NoConnectionXAML��$&�� Images/icons8-no-internet-24.png��$��80��$��80��$	��Top=�$��Right=�$��0,192,124,0q��� -   $	 CloseButtonXAML��$)��#/Images/icons8-close-window-100.png��$
��Fill=�$��70��$��70��$	��Top=�$��Right=�$��10 10q�
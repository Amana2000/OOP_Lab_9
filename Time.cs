using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
	class Time
	{
		public int seconds, minutes, hours;

		//конструктор по умолчанию 
		public Time()
		{
			hours = 0;
			minutes = 0;
			seconds = 0;
		}

		//конструктор с параметрами
		public Time(int hours, int minutes, int seconds)
		{
			Set_time(hours, minutes, seconds);
		}

		//ввод секунд с проверкой на неправильный диапазон 
		public void Set_seconds(int _seconds)
		{
			if ((_seconds >= 0) && (_seconds < 60))
				seconds = _seconds;
			else throw new ArithmeticException("Допустимый диапазон для секунд - 0..59");
		}

		//ввод минут с проверкой на неправильный диапазон 
		public void Set_minutes(int _minutes)
		{
			if ((_minutes >= 0) && (_minutes < 60))
				minutes = _minutes;
			else throw new ArithmeticException("Допустимый диапазон для минут - 0..59");

		}

		//ввод часов с проверкой на неправильный диапазон
		public void Set_hours(int _hours)
		{
			if ((_hours >= 0) && (_hours < 24))
				hours = _hours;
			else throw new ArithmeticException("Допустимый диапазон для часов - 0..23");
		}

		//Полный ввод времени
		public void Set_time(int _hours, int _minutes, int _second)
		{
			Set_hours(_hours);
			Set_minutes(_minutes);
			Set_seconds(_second);
		}

		//выыод минут
		public int Get_minutes()
		{
			return minutes;
		}

		//вывод часов
		public int Get_hours()
		{
			return hours;
		}

		//вывод секунд
		public int Get_seconds()
		{
			return seconds;
		}


		//вывод строки времени
		public string To_Str()
		{
			return Get_hours() + ":" + Get_minutes() + ":" + Get_seconds();
		}


		// перевод в часы/минуты/секунды
		public double To_hours()
		{
			return hours + minutes / 60.0 + seconds / 3600.0;
		}

		public double To_minutes()
		{
			return hours * 60.0 + minutes + seconds / 60.0;
		}

		public double To_seconds()
		{
			return hours * 3600.0 + minutes * 60.0 + seconds;
		}


		//переполнение в разряде секунд -> прибавляет переполнение к старшему разряду	
		public void Overflow_seconds()
		{
			int t;
			if (seconds >= 60)
			{
				t = seconds / 60;
				if (t >= 1)
				{
					seconds -= 60 * t;
					minutes += t;
				}
			}
			Overflow_minutes();
		}


		//переполнение в разряде минут -> прибавляет переполнение к старшему разряду 
		public void Overflow_minutes()
		{
			int t;
			if (minutes >= 60)
			{
				t = minutes / 60;
				if (t >= 1)
				{
					minutes -= 60 * t;
					hours += t;
				}
			}
			Overflow_hours();
		}


		// переполнение в разряде часов ->  присваевает остаток от деления к часам
		public void Overflow_hours()
		{
			int t, r;
			if (hours >= 24)
			{
				t = hours / 24;
				if (t >= 1)
				{
					r = hours % 24;
					hours = r;
				}
			}
		}

		//добавление секунд
		public void Add_seconds(int _seconds)
		{
			if (_seconds >= 0)
			{
				seconds += _seconds;
				Overflow_seconds();
			}
			else throw new ArithmeticException("Секунды должны быть положительными");
		}

		//добавление минут
		public void Add_minutes(int _minutes)
		{
			if (_minutes >= 0)
			{
				minutes += _minutes;
				Overflow_minutes();
			}
			else throw new ArithmeticException("Минуты должны быть положительными");
		}

		//добавление часов
		public void Add_hours(int _hours)
		{
			if (_hours >= 0)
			{
				hours += _hours;
				Overflow_hours();
			}
			else throw new ArithmeticException("Часы должны быть положительными");
		}

		//добавление времени 
		public void Add_time(int _hours, int _minutes, int _seconds)
		{
			if ((_hours >= 0) && (_minutes >= 0) && (_seconds >= 0))
			{
				hours += _hours;
				minutes += _minutes;
				seconds += _seconds;
				Overflow_seconds();
			}
		}


		public static Time operator +(Time time1, Time time2)
		{
			Time time3 = new Time();
			time3.seconds = time1.seconds + time2.seconds;
			time3.minutes = time1.minutes + time2.minutes;
			time3.hours = time1.hours + time2.hours;

			if (time3.seconds > 59)
			{
				time3.seconds -= 60;
				time3.minutes++;
			}
			if (time3.minutes > 59)
			{
				time3.minutes -= 60;
				time3.hours++;
			}
			if (time3.hours > 23)
			{
				time3.hours -= 24;
			}
			return time3;
		}

		public static Time operator -(Time time1, Time time2)
		{
			Time time3 = new Time();
			time3.hours = time1.hours - time2.hours;
			time3.minutes = time1.minutes - time2.minutes;
			time3.seconds = time1.seconds - time2.seconds;

			if (time3.seconds < 0)
			{
				time3.seconds *= -1;
				time3.seconds -= 60;
				time3.seconds *= -1;
				time3.minutes--;
			}
			if (time3.minutes < 0)
			{
				time3.minutes *= -1;
				time3.minutes -= 60;
				time3.minutes *= -1;
				time3.hours--;
			}
			if (time3.hours < 0)
			{
				time3.hours += 24;
			}
			return time3;
		}
	}
}
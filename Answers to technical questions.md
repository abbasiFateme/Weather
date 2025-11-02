# Answers to Technical Questions

## 1. How much time did you spend on this task?
حدوداً **یک ساعت** زمان صرف شد.

---

## If you had more time, what improvements or additions would you make?
اگر زمان بیشتری داشتم، موارد زیر را اضافه یا بهبود می‌دادم:
- **Swagger documentation** کامل‌تر برای مستندسازی بهتر API  
- **Caching layer** برای جلوگیری از درخواست‌های تکراری به OpenWeatherMap  
- افزودن **احراز هویت کاربران** برای کنترل دسترسی به API  

---

## 2. What is the most useful feature recently added to your favorite programming language?
در نسخه‌های قبلی C#، primary constructor فقط برای record‌ها در دسترس بود.
اما از C# 14 به بعد، می‌توان از آن برای تمام کلاس‌ها و structها استفاده کرد.
این قابلیت باعث می‌شود تعریف سازنده و propertyها بسیار ساده‌تر و خلاصه‌تر شود.

### مثال:
```csharp
سیشارپ 13 به قبل
public class WeatherInfo
{
    public string City { get; }
    public double Temperature { get; }

    public WeatherInfo(string city, double temperature)
    {
        City = city;
        Temperature = temperature;
    }
}

C# 14 
public class WeatherInfo(string city, double temperature)
{
    public string City { get; } = city;
    public double Temperature { get; } = temperature;

    public void PrintInfo()
        => Console.WriteLine($"{City}: {Temperature}°C");
}

```
---

## 3. How do you identify and diagnose a performance issue in a production environment?
برای شناسایی و عیب‌یابی مشکلات Performance در محیط Production معمولاً مراحل زیر را انجام می‌دهم:

1. بررسی میکنم **ایندکس‌های جداول دیتابیس** در ابتدای کار  
2. تحلیل **کوئری‌های پایگاه داده** با ابزارهایی مانند **SQL Profiler**  
3. بررسی **سرویس‌ها و DI (Dependency Injection)** در برنامه برای شناسایی سربار یا چرخه‌های غیرضروری  
4. مشاهده‌ی لاگ‌ها برای یافتن درخواست‌های کند یا خطاهای مکرر  

بله، قبلاً این کار را انجام داده‌ام و علت مشکل، **عدم ایندکس‌گذاری مناسب در جداول دیتابیس** بود که منجر به کاهش سرعت در Queryها شده بود.

---

## 4. What’s the last technical book you read or technical conference you attended?
- آخرین کتاب فنی: **"Clean Architecture"** اثر *Robert C. Martin (Uncle Bob)*  
  از این کتاب یاد گرفتم که **جدا کردن منطق کسب‌وکار از جزئیات زیرساختی** (مثل پایگاه‌داده یا UI) باعث افزایش انعطاف‌پذیری و قابلیت نگهداری نرم‌افزار می‌شود.

- آخرین دوره: **دوره‌ی Design Patterns** از سایت **Biamoz.ir**  
  در این دوره با الگوهای طراحی (Singleton، Factory، Observer و ...) آشنا شدم و یاد گرفتم چطور با آن‌ها کدهای تمیزتر و قابل‌توسعه‌تر بنویسم.

---

## 5. What’s your opinion about this technical test?
به نظرم تست نویسی از بهترین روش های سنجیدن عملکرد برنامه و برنامه نویس و علاوه بر آن باعث میشود
که مطمعن شویم عملکرد برنامه با هر تغییر چه واکنشی نشان میدهد و همچنین باعث رعایت اصول اولیه و کدنویسی تمیز میشود.

---

## 6. Describe yourself in JSON format
```json
{
  "name": "Fateme Abbasi",
  "job": "Backend Developer",
  "specialization": "ASP.NET Core / C# / REST APIs",
  "experience_years": 4, 
  "age" : 26,
  "location": "Tehran, Iran",
  "languages": ["Persian", "Englisch"],
}
```

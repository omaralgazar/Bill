# فواتيري - Bill Tracker

تطبيق Blazor WebAssembly (PWA) لتسجيل ومتابعة فواتير الكهرباء والمياه والغاز.

## كيفية التشغيل محليًا (Visual Studio)

1. فك ضغط المجلد.
2. افتح `Bill.csproj` بـ Visual Studio 2022 (تأكد إن عندك Workload اسمه "ASP.NET and web development").
3. اضغط F5 أو Run. أول مرة هيعمل Restore للباكدجات تلقائيًا (يحتاج نت).
4. هيفتحلك المتصفح على `localhost` وتقدر تجرب التطبيق كامل: إضافة/تعديل/حذف فواتير + الداشبورد.

## هيكل المشروع

```
Bill/
 ├── Models/BillItem.cs       // نموذج الفاتورة + إحصائيات الداشبورد
 ├── Services/BillService.cs  // كل عمليات CRUD + حساب الإحصائيات (LocalStorage)
 ├── Pages/
 │    ├── Home.razor          // الداشبورد الرئيسي (3 كروت: كهرباء/مياه/غاز)
 │    ├── CategoryList.razor  // ليستة فواتير كاتاجوري معينة
 │    └── AddEditBill.razor   // فورم موحد للإضافة والتعديل والحذف
 ├── Shared/MainLayout.razor
 └── wwwroot/                 // index.html, manifest.json, service-worker, css
```

## قبل النشر على GitHub Pages - مهم جدًا

في ملف `wwwroot/index.html` فيه السطر:
```html
<base href="/Bill/" />
```

لازم يبقى فيه **نفس اسم الريبو بتاعك بالظبط** على GitHub. يعني لو سميت الريبو `bill-tracker` مثلاً، غيّرها لـ:
```html
<base href="/bill-tracker/" />
```

## خطوات النشر على GitHub Pages

1. اعمل Repository جديد على GitHub (public).
2. من جهازك، جوه فولدر المشروع:
   ```bash
   dotnet publish -c Release -o publish
   ```
3. هتلاقي الملفات الجاهزة في `publish/wwwroot`.
4. ارفع محتوى `publish/wwwroot` بس (مش المجلد نفسه) على branch اسمه `gh-pages` في الريبو.
5. من إعدادات الريبو (Settings > Pages)، اختار المصدر (Source) branch `gh-pages`.
6. هتاخد رابط شكله: `https://username.github.io/repo-name/`
7. افتح الرابط من متصفح الموبايل (Chrome) → قائمة المتصفح (⋮) → "Add to Home Screen" / "تثبيت التطبيق".

### ملاحظة على ملف .nojekyll
GitHub Pages بيتجاهل أي فولدر اسمه بيبدأ بـ underscore (زي `_framework`) لو مفعّل عليه Jekyll افتراضيًا.
لازم تضيف ملف فاضي اسمه `.nojekyll` جوه `publish/wwwroot` قبل ما ترفعه، وإلا التطبيق مش هيفتح صح.

```bash
touch publish/wwwroot/.nojekyll
```

## البيانات

البيانات بتتخزن في LocalStorage بتاع المتصفح (على الجهاز بتاعك فقط)، وشغالة بالكامل أوفلاين بعد أول تحميل. مفيش أي سيرفر أو مزامنة بين أجهزة في النسخة دي.

## الأيقونات

الأيقونات المرفقة (`icon-192.png`, `icon-512.png`) بسيطة placeholder، تقدر تستبدلها بأيقونة التصميم اللي تحبه بنفس المقاسات.

# ASP.NET Core MVC Tabanlı Kuaför / Berber Salonu İşletme Yönetim Uygulaması

## Projenin Amacı
 Web Programlama dersinde teorik ve pratik olarak öğrenilen bilgilerin, gerçek bir probleme uygulanarak bu probleme çözüm üreten bir web projesi geliştirilmesidir.

## Projenin Ortakları
 B231210355    -    Emir Kır <br>
 G231210355    -    Metahan Gülşer

 ## Proje Özeti 

Bu projede, ASP.NET Core MVC ile bir Kuaför/Berber Salon Yönetim Sistemi geliştirilmesi amaçlanmaktadır. Sistem, salonların sunduğu hizmetlerin sürelerini ve ücretlerini, 
çalışanların uzmanlık alanlarını ve müsaitlik durumlarını yönetebilecek şekilde tasarlanacaktır. Kullanıcılar, uygun çalışanları seçerek randevu oluşturabilecek, 
bu sayede salonlar hem çalışanların verimliliğini hem de günlük kazançlarını izleyebilecektir. Projenin bir bölümünde, veri tabanı ile etkileşim için REST API kullanılacak 
ve yapay zeka entegrasyonu sayesinde kullanıcılar, fotoğraf yükleyerek saç modeli veya renk önerileri alabileceklerdir.

## Projedeki Konsept ve Gereksinimler

### 1. Kuaför ve Berber Tanımlamaları
- Uygulama, kuaför ve/veya berber salonlarının yetki çerçevesinde tanımlanmasına olanak sağlayacaktır. (Tek bir kuaför ya da berber üzerinden işlem yapılması da desteklenecektir.)
- Her salonun çalışma saatleri, sunduğu hizmetler, bu hizmetlerin süreleri ve ücretleri detaylı bir şekilde sistemde tanımlanabilir olacaktır.

### 2. Çalışan Yönetimi
- Salonlarda görev yapan çalışanlar sisteme eklenebilecektir.
- Her çalışanın uzmanlık alanları ve gerçekleştirebileceği hizmetler tanımlanabilecektir.
- Çalışanların uygunluk saatleri sisteme kaydedilecek ve müşteriler bu bilgilere göre randevu oluşturabilecektir.

### 3. Randevu Sistemi
- Kullanıcılar, uygun çalışan ve hizmet seçeneklerine göre sistem üzerinden randevu alabilecektir.
- Randevu saati, mevcut randevularla çakışıyorsa sistem kullanıcıyı bilgilendirecektir.
- Randevu bilgileri (hizmet türü, süre, ücret) sistemde kaydedilecektir.
- Randevular onay mekanizması ile desteklenerek kontrol edilecektir.

### 4. REST API Kullanımı
- Projenin belirli bir kısmında veri tabanı ile iletişim, REST API aracılığıyla sağlanacaktır.

### 5. Yapay Zeka Entegrasyonu
- Sistem, yapay zeka entegrasyonu içeren bir özellik sunacaktır.
- Kullanıcılar, sisteme fotoğraf yükleyerek yapay zeka yardımıyla saç kesim modelleri veya saç rengi önerileri alabilecektir.

## UML Şeması

![UML Diyagramı](https://github.com/Metehanglsr/Web-Programlama/raw/main/images/BarberAppUML.png)

## Kullanılacak Teknolojiler

- Asp.Net Core 6 MVC  veya daha yukarı sürümleri 
- C# 
- Veritabanı olarak SQL Server /PostgreSQL/ vb 
- Entity Framework Core ORM 
- Bootstrap Tema 
- HTML5, CSS3, Javascript ve JQUERY

## Projenin Hedefleri

- Uygulama, kullanıcı dostu bir **front-end** arayüze sahip olacaktır.
- Yönetim işlemleri için kapsamlı bir **admin paneli** oluşturulacaktır.
- Kullanıcıların üye olabileceği bir **kayıt sayfası** tasarlanacaktır.
- Sistemde **admin** ve **kullanıcı üyeler** olmak üzere iki farklı üye türü olacaktır. <BR>
    -  **Admin Kullanıcı Adı:** OgrenciNuramarasi@sakarya.edu.tr <BR>
    -  **Admin Şifre:** sau 
- **Yetkilendirme (authorization)** işlemleri doğru ve güvenilir bir şekilde uygulanacaktır.
- Projede kullanılan veri tabanı ile ilgili uygun sorgulamalarda **LINQ** kullanılarak bir API hizmeti sunulacaktır.

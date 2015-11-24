# SmallTemplateProject
============

**Mail: f.gurdal@hotmail.com.tr, fatihgurdal@fatihgurdal.net**

**udemy: https://www.udemy.com/user/fatihgrdal/**

**Web: http://fatihgurdal.net/**

**Blog2: https://fatihgurdal.wordpress.com/**


Proje Kapsamı: Bu proje Bilgisayar Öğretmeni olan Junior Web Developer tarafından yapılmıştır(Ne kadar yazılımcı ararken illede mühendis olsun haal var ama!). 6 aylık bir .net eğitimden sonra gelişip değişen bir devepoler. Bu mimari ufak ve orta dereceli projeler için geliştirilmiştir(Sizin ufak ve orta kapsamınız nedir bilemem ama neyse). Katmanli mimari kullanılarak SOLİD prensiplerine uygun bir proje geliştirme şansı sunmaktadır. SOLİD pirensiplerinden "İ" (İnterface Segregation Principle) harfine tekabul eden interface ler ile çalışma yoktur. Bunun sebebi ise bazı projelerde farklı sınıflar arasında geçişlerin olmayacağı projeler bulunmaktadır. İlerleyen safalarda Aynı projenin castle windsor kullanılarak bu prensipli versiyonunu çıkartacağım farklı bir mimari olacak ve ismide fakrlı olarak orta ve büyük projeler için kullanılmak üzere geliştirilecektir.

Giriş bölümüde çok uzadı ama neyse :) Neyse çok konuştum gel gelelim bu mimari bize neler sağlıyor.

# Katmanaların Görevleri Neler?
### MVCProject
1- Filter'ları açmak için Actionların üzerine attribute olarak ekleyiniz. 3 Adet filter vardır.

1-1-AuthorizationFilter => Oturum kontrolü yapar eğer oturumu yoksa Login'e yönlendirir. Oturum olup sayfaya yetkisi yoksa sayfa içerğini göndermez.

1-2-LogFilter => Sayfa içi gezinmeleri DB ye ekler

1-3-ErrorFilter => Sayfada bir hata gelmesi yada olmayan bir sayfaya gidildiğinde kullanıcıya gösterilecek sayfa için hazırlanmıştır.
	
2- Hazır popup methodları

3- Get ve Post Scriptleri 

4- Custom Helper Method (App_Data) 

5- Login ve Yönetim Paneli

6- Yetkiye göre dinamik menü

### SmallProject.Business
1-Her Entity'nin bir iş sınıfı olmalıdır. Ve bu iş sınıflarında tüm methodlar yazılmalıdır. UI katmanında extra iş yapmadan bu methodlar kullanılarak işlemler yapılmalıdır. Bunu sağlamak adınada SOLİD prensiplerine uyulmalıdır.

2- Her sınıf için Context'e erişmek için "private Repository<Entity> db { get; set; }" tanımlanarak oluşturucu method da ram'den referans almasını sağlayınız.

3- Business yani iş katmanının göreci UI ile Context aracılığı yapmaktır. UI kesinlikle direk Database'e erişemez bir aracı olmalıdır bu aracıda Business katmanıdır.

### SmallProject.Context
1- MyContex Database nesnemizdir. İçersinde tüm tabloları barındırmaktadır.

2- Her Class "EntityBase" den türemektedir (Özel class lar hariç Log, ErrorLog gibi) çünkü context'te savechange metohdunuz ezdiğimizde gelen nesneleri EntityBase'den miras alınmış ise 4. maddedeki işlemleri yapacaktır.

3- Seed Methodu ile DB ilk kez oluşturulduğunda örnek verileri ekler. Bu kısmı lütfen silmeyiniz çünkü başlangıç verileri mutlaka olmalıdır. Sadece düzenelem ve ekleme yapınız. İlk kullanıcı her zaman en üst yetkilidir bir yetki atanmasa dahi o yetkiye sahip olarak kabul edilir. 

4- SaveChange() methodu ezilmiştir ve hiç bir veri silinemez hale getirilmiştir. 
	*Silme işlemleri update e çevirilerek IsDeleted alanı True'ya çevrilmektedir. 
	*Güncellenme işleminde düzenleme tarihi ve düzenleyen son kişi otomatik eklenmektedir.
	*Ekleme işleminde ise oluşturma tarihi ve düzenleyen son kişi otomatikk eklenmektedir.

### SmallProject.Entity
1- En entity "EntityBase" den miras almalıdır.

2- Hiç bir entitye Id konulmamalıdır. EntityBase tüm tablolarda olması gereken kolonları barındırmaktadır.

3- Sadece Log ve ErrorLog tablosu entity baseden miras almaz. Bunun sebebide silinemez, değiştirilmez ve eklenemez bir alandır. Kullanabilmek için LogFilter ve ErrorFilter aktif hale getirilir ve sistem tarafından otomaitk eklenir. Sistem yöneticileri sadece okuma yapabilir ve özel durumlar için silme yapılabilir.

4-Dataannotations aktif olarak kullanılmaktadır. Bir nesne ekrana basıldığında nasıl bir formatta basılacağı yada ekrana basılmayacak nesneler varsa bunları belirtmek, DB oluştururken kolan tip ve ayarları ve UI katmanındaki inputların validationsları bu şekilde ayarlanmaktadır. Ek olarak mapping yoktur !
Özetle bu katman fasülye katmandır tüm katmanlarda bu referans alınmalıdır.


# Bir istek geldiğinde iş akışı nasıl ilerlemektedir?
1- Çalışan proje MVCProject katmanıdır. İsteklr bu katmandan yollanır ve kullanıcılarımız bu katmanda işlemler yapmaktadır.

2- MVC katmanına gelen istek iş katmanına(SmallProject.Business) gönderilir burada ilgili method yada methodlar çalışır

3-Database işlemi için Veritabanı katmanı (SmallProject.Context) gönderilir bu katman kodlar düzenleme işlem yapma kodları ile uğraşmaz direk SQL kodları alır ve DB de gerekli işlemleri yapar. İstediği verileri iş katmanına yollar. 

4-İş katmanı aldığı veriler ile gerekli işlemlerini yaptıktan sonra MVC katmanına yollar.

5- MVC katmanına kullanıcıya gösterilmesi gereken HTML kodlarını yansıtır.
Özetlersek Kullanıcı>MVC>Business>Context>Business>MVC>Kullanıcı 

# Bizleri hangi iş yüklerinden kurtarmaktadır?
1- Katmanlara bölme

2- Otomatik verinlerin değişimleri, eklenmelerinin takibi,

3- Her tabloya eklenen kolanları sistematik hale getirme

5- Her sayfada html elementlerin label larını yazmaktan kurtarıyor ve her sayfa için değil bir nesneye 1 kez label'ı ne oalcağı belirtiliyor.

6- Otomatik Jquery Validation

7- Hazır Get & Post Methodları

8- Dinamik Menü

9- Dinamik Oturum kontrolü

10- Hata yönetimi

11- Kullanıcıların gezinli loglaması

12- Hali hazırda Yönetim paneli şablonunun düzenlenip hazırlanan Layout(webforms da masterpage olarak bilinir)

13-Responsive bir yönetim paneli

14- Geniş widget ve icon setleri

15- Sayfalama, filtreleme ve sıralaması olan bir Grid Framework'ü yüklenmiş ve örnek kullanımı


**Evet değerli geliştirici arkadaşlarım eğer beğendiyseniz o zaman kullanalım :) Nasıl kullanacağınızdan da kısaca bahsedelim;**

Şimdi MVC katmanındaki Connection String'i değiştiriyorsunuz sonra F5 e basıyorsunuz... Bitti bu kadar :))) Her şey içersinde hazırdır tüm methodların açıklama satırları ve katmanları ne işe yaradığı belirtilmektedir. Tek yapmanız gereken Bu methodları kullanarak kendi projenize has sayfaları tasarlamaktır.

Bu arada.... Bu mimari yönetim sistemi içindir kişisel site, kurum siteleri vs. değildir sonuçta arayüzünüzüde tahmin edemem dimi ;) Siz sadece projenizin yönetim kısmını için şimdilik bir kaç sayfa hali hazırda var dinamik öenü gelmsi kullanıcıları listeleme ekleme vs. zamanla yeni check-in ler ile büyüreceğim. Kullanan arkadaşlar geni dönüş yapar ise çok memnun kalırım... Kolay gele ;)

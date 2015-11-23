//Kullanılabilmesi için JQoeru kütüphanesi olmalıdır

//Sayfa sayfa yüklendiğinde sabit değerlen alacak değişkenlerimiz.
formData = new FormData();
HtmlLoadingIcon = "<img src=\"/Images/loading_blue.GIF\"  class=\"img img-responsive\"  style=\"margin: auto;\" />";


// --------------------------------------------------------------> //

// <summary>
// Bir Action'a istdiğiniz parametrler yollanarak istek atmak için kullanılır.
// </summary>
// <param name="addess">İstek Atacağınız Link - string olarak gönderilmelidir.</param>
// <param name="selectorObj">Geri dönen verileri bir elementin içine basmak istiyorsanız, bascağınız elementin Jqeury nin anlayacağı şekilde bir selector yazınız.</param>
// <param name="metod">İsteğin olumlu sonuçlanması ile çalıştırmak istediğiniz bir function yollayınız.</param>
// <param name="animations">Bu istek başladığında ve bittiğinde ekranda bir animasyon göstermek istiyorsanız o animasyonun Jquery selectorünü yazınız. Sistem Show Hide yapıcatır.</param>
function GetRequest(addess, selectorObj, metod, animations) {
    if (animations) {
        $("#AnimationElement").show();
    }
    $.ajaxSetup({ scriptCharset: "utf-8", contentType: "application/json; charset=utf-8" });
    $.get(adres, $.param(params, true), function (data) {
        if (selectorObj != null)
            $(selectorObj).html(data);
        if (metod != null)
            metod(data);
    }).error(function (jqXHR, textStatus, errorThrown) {
        alert("Hata Mesajı:" + errorThrown);
    }).complete(function () {
        $("#AnimationElement").hide();
    });
    $.ajaxSetup({ scriptCharset: "utf-8", contentType: "application/x-www-form-urlencoded; charset=UTF-8" });
}

// <summary>
// Form adı ile çersindeki verileri otomatikman parametreler gönüştürür ve PostForm ile isteği atarsınız.
// </summary>
// <param name="formnames">Parametreye çevirmek istediğin form</param>
// <param name="clearFormData">Daha önceki verileri temizleyerek yapmak istiyor isen Bool tipinde. Eğer temizlemezsen birden fazla addParametersForms kullanıyor isen onlar birleşir.</param>
function addParametersForms(formNames, clearFormData) {
    if (clearFormData) {
        formData = new FormData();
    }
    for (var j = 0; j < formNames.length; j++) {
        var poData = jQuery(document.forms[formNames[j]]).serializeArray();
        for (var i = 0; i < poData.length; i++) {
            formData.append(poData[i].name, poData[i].value);
        }
    }
}
/// <summary>Bu fonsiyon post isteği attıksan sonra olumlu sonuçlar için bir fonsiyon çalıştırmak içindir.</summary>
/// <param name="PostURL" type="Number">İsteğin atılacağı URL</param>
/// <param name="method">Eğer isterseniz function(data) şeklinde fonksiyon tanımlayin yoksa Null atayınız</param>
function PostFormV2(PostURL, method) {
    $.ajaxSetup({ scriptCharset: "utf-8", contentType: "application/json; charset=utf-8" });
    $.ajax({
        url: PostURL,
        type: "POST",
        data: JSON.stringify(formData),
        contentType: false,
        cache: false,
        processData: false,
        success: function (data) {
            if (method != null)
                method(data);
        },
        error: function () {

        }
    });
    $.ajaxSetup({ scriptCharset: "utf-8", contentType: "application/x-www-form-urlencoded; charset=UTF-8" });
}
/// Şimdilik sade bir alert basmaktadır. İler ki sürümlerde komplex bir yapıda alert planlanmaktadır.
function stpAlert(message,statu) {
    alert(message);
}
/// <summary>Bir içeriği Modal içersinde göstermek istiyor iseniz bu methodu kullanınız</summary>
/// <param name="ViewPath" type="Number">İsteğin atılacağı URL</param>
/// <param name="ModalTitle">Modal Başlık</param>
/// <param name="ModalWidth">Açılacak olan Modal 'ın %(yüzde) yada PX cinsinden genişliği.</param>
function ViewGet(ViewPath, ModalTitle, ModalWidth) {
    if ((typeof ModalWidth !== "undefined") || (ModalWidth != null)) {
        $("#ModalDialogId").css("width", ModalWidth);
    } else {
        $("#ModalDialogId").css("width", "350px");
    }
    $("#SpeedmeModal .modal-body").html(HtmlLoadingIcon);
    $("#SpeedmeModal").modal("show");
    GetRequest(ViewPath, "#HızlıEklemeModal .modal-body", null);
    $("#SpeedmeModal .modal-title").text(ModalTitle);
}

function sessionControl() {
    $.ajax({
        url: "/Account/LoginControl",
        type: "POST",
        contentType: false,
        cache: false,
        processData: false,
        success: function (data) {
            if (!data) {
                location.reload();
                //Yada
                //window.location="/Account/Login";
            }

        },
        error: function () {
            location.reload();
            //Yada
            //window.location="/Account/Login";
        }
    });

}
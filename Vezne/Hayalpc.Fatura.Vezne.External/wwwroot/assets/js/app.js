function AreYouSure() {
    return confirm("Emin misiniz?");
}

//@onchange = "return loadCity(this.value);" 
function loadCity(countryId) {
    var mCountryId = $("#CountryId").val();
    if (mCountryId)
        countryId = mCountryId;
    var CityId = $("#CityId").data("default");
    console.log(CityId);
    var DistrictId = $("#DistrictId").data("default");

    if (countryId != undefined && countryId > 0) {
        var filter = '["countryId", "=", ' + countryId + ']';
        $.post("/parameters/city", { filter: filter }, function (result) {
            var list = result.data;
            $("#CityId option:not(:first)").remove();
            if (DistrictId == 0) {
                $("#DistrictId option:not(:first)").remove();
            }
            $.each(list, function (i, data) {
                var selected = CityId == data.id ? "selected" : "";
                var option = "<option " + selected + " value='" + data.id + "'>" + data.name + " (" + data.id + ")</option>";
                $("#CityId").append(option);
            });
        });
    }

}

function loadDistrict(cityId) {
    var mCityId = $("#CityId").val();
    if (mCityId)
        cityId = mCityId;
    var DistrictId = $("#DistrictId").data("default");
    console.log(DistrictId);
    if (cityId != undefined && cityId > 0) {
        var filter = '["cityId", "=", ' + cityId + ']';
        $.post("/parameters/district", { filter: filter }, function (result) {
            var list = result.data;
            $("#DistrictId option:not(:first)").remove();
            $.each(list, function (i, data) {
                var selected = DistrictId == data.id ? "selected" : "";
                var option = "<option " + selected + " value='" + data.id + "'>" + data.name + " (" + data.id + ")</option>";
                $("#DistrictId").append(option);
            });
        });
    }
}

function loadAjax(el) {
    var url = el.data("controller");
    var defaultVal = el.data("default");
    if (url != undefined && url.length > 0) {
        var data = {

        };
        $.post(url, data, function (result) {
            var list = result.data;
            $.each(list, function (i, data) {
                var selected = defaultVal == data.id ? "selected" : "";
                var option = "<option " + selected + " value='" + data.id + "'>" + data.name + " (" + data.id + ")</option>";
                $(el).append(option);
            });
        });
    }
}

function changeLanguage(el) {
    $.get("/change-language/" + $(el).data("lan"), function () { window.location.reload(); });
}

function checkboxOpenPanel(el) {
    if ($(el).prop("checked"))
        $("." + $(el).attr("name")).slideDown();
    else
        $("." + $(el).attr("name")).slideUp();
}

function dropdownOpenPanel(el) {
    $("." + $(el).attr("name")).slideUp();
    if ($(el).val() != "")
        $("." + $(el).attr("name") + "-" + $(el).val()).slideDown();
}

$(function () {

    if ($("[role='form']").validate) {
        $("[role='form']").validate();
    }

    if ($('.select2').select2) {
        $('.select2').select2();
    }
    if ($('.decimalReverse').mask) {
        $('.decimalReverse').mask('#0,00', { reverse: true });
    }

    if ($('[data-toggle="tooltip"]').tooltip) {
        $('body').tooltip({ selector: '[data-toggle="tooltip"]' });
    }

    $(".loadAjax").each(function (k, v) {
        loadAjax($(this));
    });

    $('#hover-btn').hover(function () {
        //$(this).find(".dropdown-toggle").dropdown('toggle');
    });

})  
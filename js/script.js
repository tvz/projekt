//za popup prozor prilikom povrata kor.imena ili lozinke
function popup(url) {
    var width = 500;
    var height = 300;
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    var params = 'width=' + width + ', height=' + height;
    params += ', top=' + top + ', left=' + left;
    params += ', toolbar=no';
    params += ', menubar=no';
    params += ', resizable=no';
    params += ', directories=no';
    params += ', scrollbars=no';
    params += ', status=no';
    params += ', location=no';
    newwin = window.open(url, 'd', params);
    if (window.focus) {
        newwin.focus()
    }
    return false;
}

//jquery
$(document).ready(function () {
    //za jquery slide div
    $(".poravnanje").slice(1).toggle();

    $("#prikazi_opcije").click(function () {
        $(".poravnanje").slice(1).slideToggle("slow");
        if ($("#prikazi_opcije").text() == 'Prikaži dodatne opcije') {
            $("#prikazi_opcije").text("Sakrij dodatne opcije");
        }
        else {
            $("#prikazi_opcije").text("Prikaži dodatne opcije");
        }
        return false;
    });

    //za searchBox na masteru
    $('#searchBox').focus(function (e) {
        if (e.target.value == e.target.defaultValue)
            e.target.value = '';
    });
    $('#searchBox').blur(function (e) {
        if (e.target.value == '')
            e.target.value = e.target.defaultValue;
    });
});
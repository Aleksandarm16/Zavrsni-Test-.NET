
$(document).ready(function () {
    var host = window.location.host;
    var token = null;
    var headers = {};
    var passengerEndpoint = "/api/Passenger/";
    var busEndpoint = "/api/Bus/";


   loadPassengers();
   loadBus();



    $("body").on("click", "#regbtnswitch", showRegistration);
    $("body").on("click", "#dismisbtninput", showStart);
    $("body").on("click", "#logbtnswitch", showLogin);
    $("body").on("click", "#btnlogout", logoutuser);
    $("body").on("click", "#btnDelete", deletePassenger);
    $("body").on("click", "#clearaddform", clearAddForm);



    function loadPassengers() {
        var requesturl = 'https://' + host + passengerEndpoint;
        $.getJSON(requesturl, setPassengers);
    }
    function loadBus() {
        var requesturl = 'https://' + host + busEndpoint;
        $.getJSON(requesturl, setBus);
    }

    function setBus(data, status) {
        var $container = $("#inputBus");
        $container.empty();
        if (status == "success") {
            for (i = 0; i < data.length; i++) {
                var value = $("<option value=" + data[i].Id + " >" + data[i].Line + "</option > ");
                $container.append(value);
            }

        }
    }

    function setPassengers(data, status) {
        var $container = $("#Passengersinfo");
        $container.empty();

        if (status == "success") {

            var div = $("<div style='width:1000px;margin:auto;margin-top:40px;'></div>");
            var h1 = $("<h4 class='text-center'><b>Putnici</b></h4>");
            div.append(h1);
            var table = $("<table class='table table-bordered'></table>");
            if (token) {
                var header = $("<thead style='background:greenyellow;'><tr><td>Ime i prezime</td><td>Adresa</td><td>Godina rodjenja</td><td>Linija autobusa</td><td>Tip karte</td><td style='width=100px;'>Akcija</td></tr></thead>");
            }
            else {
                var header = $("<thead style='background:greenyellow;'><tr><td>Ime i prezime</td><td>Adresa</td><td>Godina rodjenja</td><td>Linija autobusa</td></tr></thead>");
            }
            table.append(header);
            var tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {

                var row = "<tr>";
                var displayData = "<td>" + data[i].NameAndLastName + "</td><td>" + data[i].Adress + "</td><td>" + data[i].Year + "</td><td>" + data[i].BusLine + "</td>";

                var stringId = data[i].Id.toString();
                var displayDelete = "<td><input style='width:80px;' class='btn btn-default col-sm-4' id=btnDelete name=" + stringId + " value=Obrisi /></td>";
                var displayType = "<td>" + data[i].CardType + "</td>";

                if (token) {
                    row += displayData + displayType + displayDelete + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }


                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);



            $container.append(div);
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja putnika!</h1>");
            div.append(h1);
            $container.append(div);
        }


    }

    function deletePassenger() {
        var deleteId = this.name;
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        $.ajax({
            url: 'https://' + host + passengerEndpoint + deleteId.toString(),
            type: "DELETE",
            headers: headers
        }).done(function (data) {
            refreshTable();
        }).fail(function (data) {
            alert("Neuspesno brisanje!");
        });

    }

    $("#searchform").submit(function (e) {
        e.preventDefault();
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        if (searchend() && searchstart()) {


            var startval = $("#inputStartSearch").val();
            var endval = $("#inputEndSearch").val();
            var start = parseInt(startval);
            var end = parseInt(endval);

            var sendData = {
                "Start": start,
                "End": end
            };
            $.ajax({
                type: "POST",
                url: 'https://' + host + "/api/" + "pretraga",
                headers: headers,
                data: sendData
            }).done(function (data, status) {
                setPassengers(data, status);
                $("#inputStartSearch").val("");
                $("#inputEndSearch").val("");
            }).fail(function (data) {
                alert("Nisu pronadjeni putnici");
            });
        }
        else {
            alert("Uneta polja moraju biti dobra!");
        }


    });

    $("#addPassengerform").submit(function (e) {
        e.preventDefault();
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        if (passengername() && passengeradress() && passengertype() && passengeryear()) {



            var adress = $("#inputAdress").val();
            var year = $("#inputYear").val();
            var name = $("#inputName").val();
            var cardtype = $("#inputCardType").val();
            var busline = $("#inputBus option:selected").val();



            var sendData = {
                "NameAndLastName": name,
                "Year": year,
                "Adress": adress,
                "CardType": cardtype,
                "BusId": busline
            };

            $.ajax({
                type: "POST",
                url: "https://" + host + passengerEndpoint,
                headers: headers,
                data: sendData
            }).done(function (data, status) {
                refreshTable();
            }).fail(function (data, status) {
                alert("Greska prilikom dodavanja putnika!");
            });
        }
        else {
            alert("Niste dobro popunili polja, putnik nije dodat!");
        }



    });

    function clearAddForm() {
        $("#inputAdress").val("");
        $("#inputYear").val("");
        $("#inputName").val("");
        $("#inputCardType").val("");
        $("#nameerro").text("");
       $("#adresserr").text("");
       $("#Yearerro").text("");
       $("#typeerro").text("");
     
    }

    function logoutuser() {


        token = null;
        headers = {};
        $("#logeduserinfo").empty();
        $("#searchform").removeClass("shown");
        $("#addPassengerform").removeClass("shown");
        $("#addPassengerform").addClass("hidden");
        $("#searchform").addClass("hidden");
        showStart();
        refreshTable();
    }


    $("#registrationform").submit(function (e) {
        e.preventDefault();
        if (username2() && passwordreg1() && passwordreg2()) {


            var email = $("#inputEmailregist").val();
            var pass1 = $("#inputPasswordreg1").val();
            var pass2 = $("#inputPasswordreg2").val();


            var sendData = {
                "Email": email,
                "Password": pass1,
                "ConfirmPassword": pass2
            };

            $.ajax({
                type: "POST",
                url: 'https://' + host + "/api/Account/Register",
                data: sendData

            }).done(function (data) {

                showLogin();
                refreshTable();

            }).fail(function (data) {

                alert("Greska prilikom registracije!");
            });
        }
        else {
            alert("Uneta polja moraju biti dobra!");
        }

    })
    $("#loginform").submit(function (e) {
        e.preventDefault();
        if (username() && password()) {


            var email = $("#inputEmaillogin").val();
            var pass = $("#inputPasswordlogin").val();
            var sendData = {
                "grant_type": "password",
                "username": email,
                "password": pass
            };
            $.ajax({
                "type": "POST",
                "url": 'https://' + host + "/Token",
                "data": sendData
            }).done(function (data) {
                token = data.access_token;
                hideloginandregistration();
                $("#logeduserinfo").append("<p class='text-center'>Prijavljen korisnik:   " + "<b>" + data.userName + "</b>" + "</p>");
                $("#logeduserinfo").append("<div class='row' style='margin-top:20px;display:flex;justify-content:center;'><input type='button' class='btn col-sm-3' value='Odjava' id=btnlogout style='background-color: greenyellow;'></div>");


                $("#addPassengerform").removeClass("hidden");
                $("#addPassengerform").addClass("shown");
                $("#searchform").removeClass("hidden");
                $("#searchform").addClass("shown");

                refreshTable();
            }).fail(function (data) {
                alert("Greska prilikom prijave!");
            });
        }
        else {
            alert("Uneta bolja moraju biti dobra!");
        }

    })
    function showStart() {
        $("#registrationform").removeClass("shown");
        $("#registrationform").addClass("hidden");
        $("#loginform").removeClass("shown");
        $("#loginform").addClass("hidden");
        $("#logininfo").removeClass("hidden");
        $("#logininfo").addClass("shown");
        $("#inputEmaillogin").text("");
        $("#inputPasswordlogin").text("");
        $("#inputEmailregist").text("");
        $("#inputPasswordreg1").text("");
        $("#inputPasswordreg2").text("");
        $("#loginusernameerr").text("");
        $("#loginpasserr").text("");
        $("#regusernameerr").text("");
        $("#regpass1err").text("");
        $("#regpass2err").text("");


    }
    function showRegistration() {
        $("#registrationform").removeClass("hidden");
        $("#registrationform").addClass("shown");
        $("#logininfo").removeClass("shown");
        $("#logininfo").addClass("hidden");
    }
    function showLogin() {
        $("#registrationform").removeClass("shown");
        $("#registrationform").addClass("hidden");
        $("#loginform").removeClass("hidden");
        $("#loginform").addClass("shown");
        $("#logininfo").removeClass("shown");
        $("#logininfo").addClass("hidden");
    }
    function hideloginandregistration() {
        $("#registrationform").removeClass("shown");
        $("#registrationform").addClass("hidden");
        $("#logininfo").removeClass("shown");
        $("#logininfo").addClass("hidden");
        $("#loginform").removeClass("shown");
        $("#loginform").addClass("hidden");
    }


    //Validacija
    //Menjati, input za add formu
    function passengername() {
        var name = $("#inputName").val();
        if (name != "" && typeof name === 'string' && name.length < 140) {

            return true;
        }
        else {
            $("#nameerro").text("Ime putnika ne sme biti prazno i mora biti manje od 140 karaktera!");
            return false;
        }
    }
    function passengeradress() {
        var adress = $("#inputAdress").val();
       
        if (adress != "" && typeof adress === 'string' && adress.length <= 200) 
        {

            return true;
        }
        else {
            $("#adresserr").text("Adresa ne sme biti prazna i mora biti manje od 200 karaktera!");
            return false;
        }

    }
    function passengeryear() {
        var yearraw = $("#inputYear").val();
        var year = parseInt(yearraw);
        if (year >= 1900 && year < 2021 && year != NaN) {

            return true;
        }
        else {
            $("#Yearerro").text("Godina mora biti izmedju 1900 i 2021!");
            return false;
        }



    }
    function passengertype() {
        var type = $("#inputCardType").val();
        if (type != "" && typeof type === 'string' && type.length < 20) {

            return true;
        }
        else {
            $("#typeerro").text("Tip karte ne sme biti prazno i mora biti manje od 20 karaktera!");
            return false;
        }
    }
    $("#inputName").focus(() => $("#nameerro").text(""));
    $("#inputAdress").focus(() => $("#adresserr").text(""));
    $("#inputYear").focus(() => $("#Yearerro").text(""));
    $("#inputCardType").focus(() => $("#typeerro").text(""));
    $("#inputCardType").blur(passengertype);
    $("#inputName").blur(passengername);
    $("#inputAdress").blur(passengeradress);
    $("#inputYear").blur(passengeryear);
    



 
    function searchstart() {
        var startraw = $("#inputStartSearch").val();
        var start = parseInt(startraw);
        if (start == NaN) {
            $("#psearchstarterr").text("Uneta vrednost mora broj");
            return false;
        }
        if (start >= 1900) {

            return true;
        }
        else {
            $("#psearchstarterr").text("Uneta vrednost mora biti veca ili jednaka od 1900!");
            return false;
        }
    }
    function searchend() {
        var startraw = $("#inputStartSearch").val();
        var start = parseInt(startraw);
        var endraw = $("#inputEndSearch").val();
        var end = parseInt(endraw);
        if (end == NaN) {
            $("#psearchenderr").text("Uneta vrednost mora broj");
            return false;
        }
        if (end > start && end < 2021) {

            return true;
        }
        else if (start > end) {
            $("#psearchenderr").text("Uneta vrednost mora biti veca od pocetne " + start);
        }
        else {
            $("#psearchenderr").text("Uneta vrednost mora biti manja od 2021");
            return false;
        }
    }

    function username() {
        var username = $("#inputEmaillogin").val();
        if (username == "") {
            $("#loginusernameerr").text("Uneto korisnicko ime ne sme da bude prazno");
            return false;
        }
        else {
            return true;
        }

    }
    function password() {
        var password = $("#inputPasswordlogin").val();
        if (password == "") {
            $("#loginpasserr").text("Uneta sifra ne sme da bude prazna!");
            return false;
        }
        return true;

    }
    function username2() {
        var username = $("#inputEmailregist").val();
        if (username == "") {
            $("#regusernameerr").text("Uneto korisnicko ime ne sme da bude prazno");
            return false;
        }
        return true;
    }
    function passwordreg1() {

        var password = $("#inputPasswordreg1").val();
        if (password == "") {
            $("#regpass1err").text("Uneta sifra ne sme da bude prazna!");
            return false;
        }
        return true;
    }
    function passwordreg2() {

        var password1 = $("#inputPasswordreg2").val();
        var password2 = $("#inputPasswordreg1").val();
        if (password1 == "") {
            $("#regpass2err").text("Uneta sifra ne sme da bude prazna!");
            return false;
        }
        if (password1 != password2) {
            $("#regpass2err").text("Uneta sifra mora da se poklapa!");
            return false;
        }
        return true;
    }
    $("#inputStartSearch").blur(searchstart);
    $("#inputEndSearch").blur(searchend);
    $("#inputEmaillogin").blur(username);
    $("#inputPasswordlogin").blur(password);
    $("#inputEmailregist").blur(username2);
    $("#inputPasswordreg1").blur(passwordreg1);
    $("#inputPasswordreg2").blur(passwordreg2);
    $("#inputStartSearch").focus(() => $("#psearchstarterr").text(""));
    $("#inputEndSearch").focus(() => $("#psearchenderr").text(""));
    $("#inputEmaillogin").focus(() => $("#loginusernameerr").text(""));
    $("#inputPasswordlogin").focus(() => $("#loginpasserr").text(""));
    $("#inputEmailregist").focus(() => $("#regusernameerr").text(""));
    $("#inputPasswordreg1").focus(() => $("#regpass1err").text(""));
    $("#inputPasswordreg2").focus(() => $("#regpass2err").text(""));


    function refreshTable() {
       
        $("#inputAdress").val("");
        $("#inputYear").val("");
        $("#inputName").val("");
        $("#inputCardType").val("");



        $("#inputEmailregist").val("");
        $("#inputPasswordreg1").val("");
        $("#inputPasswordreg2").val("");
        $("#inputEmaillogin").val("");
        $("#inputPasswordlogin").val("");
        loadPassengers();

    }

});
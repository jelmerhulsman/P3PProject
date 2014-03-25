$(document).ready(function () {
    $('#addToCartButton').click(function () {
        var id = $(this).attr('class');
        $.post("http://localhost:52802/Ajax/PhotoToCart", { id: id }, function (data) {
            if ($('#slidePhoto').length == 1) {
                $('#slidePhoto').remove();
            }
            $('#main').append('<div id="slidePhoto"></div>');
            var imgPosition = $('#imageOverlay img').position();
            var boxPosition = $('#boxOverlay').position();
            $('#slidePhoto').append($('#imageOverlay img'));
            $('#slidePhoto').css({ left: imgPosition.left + boxPosition.left, top: imgPosition.top + boxPosition.top, position: 'fixed', zIndex: 20 });
            $('#overlay').fadeOut();
            $('#slidePhoto').delay(300).animate({ top: -$('#slidePhoto img').height(), opacity: 0 }, 300);

            updateCart();
        });
    });

    updateCart();
});

function updateCart() {
    $('#shoppingCartBox ul li').each(function () {
        $(this).remove();
    })
    $.post("http://localhost:52802/Ajax/GetOrder", {}, function (data) {
        var order = data;
        if (data != "" && data != null) {
            if (order.indexOf(',') == -1) {
                $.post("http://localhost:52802/Ajax/PhotoInfo", { id: order }, function (data) {
                    $('#shoppingCartBox ul').prepend(
                        '<li class="' + data.Id + '">' +
                            '<div class="cartImage">' +
                                '<img src="../../Images/Categories/' + data.Category + '/Previews/' + data.File_name + '" alt="" />' +
                            '</div>' +
                            '<div class="cartDescription">' +
                                '<p>' + data.Name + '</p>' +
                                '<p>' + data.Category + '</p>' +
                                '<p class="price">€ ' + data.Price + '</p>' +
                            '</div>' +
                            '<p class="removeItem ' + data.Id + '"></p>' +
                            '<div class="clear"></div>' +
                        '</li>'
                    );
                    if ($('#shoppingCartBox li.' + data.Id + ' img').height() < $('#shoppingCartBox li.' + data.Id + ' img').width()) {
                        $('#shoppingCartBox li.' + data.Id + ' img').addClass('landscape');
                    }
                    else {
                        $('#shoppingCartBox li.' + data.Id + ' img').addClass('portrait');
                    }
                    setCartText();
                    addRemoveFunctionality();
                    updatePrice();
                });
            } else {
                var orders = order.split(',');
                orders.forEach(function (order) {
                    if(order != ""){
                        $.post("http://localhost:52802/Ajax/PhotoInfo", { id: order }, function (data) {
                            $('#shoppingCartBox ul').prepend(
                                '<li class="' + data.Id + '">' +
                                    '<div class="cartImage">' +
                                        '<img src="../../Images/Categories/' + data.Category + '/Previews/' + data.File_name + '" alt="" />' +
                                    '</div>' +
                                    '<div class="cartDescription">' +
                                        '<p>' + data.Name + '</p>' +
                                        '<p>' + data.Category + '</p>' +
                                        '<p class="price">€ ' + data.Price + '</p>' +
                                    '</div>' +
                                    '<p class="removeItem ' + data.Id + '"></p>' +
                                    '<div class="clear"></div>' +
                                '</li>'
                            );
                            setCartText();
                            addRemoveFunctionality();
                            updatePrice();
                        });
                    }
                });
            }
        } else {
            setCartText();
        }
    });
}

function setCartText() {
    var cartButtonText = $("#shoppingCartBox ul li").length;
    if (cartButtonText == 0) {
        $('#cartPrice').prev().css({ paddingBottom: 0, marginBottom: 0, borderBottom: 0 });
        $('#cartPrice').css({ paddingBottom: '15px' });
        $('#cartPrice').html('<p>No photos selected.</p>');
        $('#cartCheckOut').hide();
    } else {
        $('#cartPrice').prev().removeAttr("style");
        $('#cartPrice').removeAttr("style");
        $('#cartPrice').html('<p>Sub total<br />Discount<br />Total</p><p class="prices">€ 00,00<br />10%<br />€ 00,00</p><div class="clear"></div>');
        $('#cartCheckOut').show();
    }

    if (cartButtonText == 1)
        cartButtonText += " Photo";
    else
        cartButtonText += " Photos";


    $(".shopping a span").html(cartButtonText);
}

function updatePrice() {
    var price = 0;
    $('#shoppingCartBox ul .price').each(function () {
        var pPrice = parseInt($(this).text().replace('€', ''));
        pPrice = pPrice;
        price += pPrice;
    })

    var discount = 0.10;
    var totalPrice = price * discount;
    totalPrice = price - totalPrice;
    console.log(totalPrice);
    $('#shoppingCartBox #cartPrice .prices').html('&euro; ' + price + '<br/>' + discount * 100 + '%<br/>&euro; ' + totalPrice);
}

function addRemoveFunctionality() {
    $(".removeItem").click(function () {
        $(this).parent("li").fadeOut(function () {
            $(this).remove();
            setCartText();
            updatePrice();

            var order = "";
            var i = 0;
            $('#shoppingCartBox ul li').each(function () {
                if (i == 0) {
                    order = $(this).attr("class");
                    i++;
                } else {
                    order = order + ', ' + $(this).attr("class");
                }
            });

            $.post("http://localhost:52802/Ajax/RemoveFromCart", { order: order }, function () { });
        });
    });
}
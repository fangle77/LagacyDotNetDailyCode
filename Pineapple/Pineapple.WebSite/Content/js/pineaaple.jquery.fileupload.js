$(function () {
    $('#addInput').click(function () {
        var maxRow = Number($('#fileInputContain').attr('maxRows')) || 10;
        if ($('#fileInputContain').find('tr').length >= maxRow) return;
        $('#fileInputContain').append($('<tr>').append($('<td>').append($('#fileInputFormat').html())));
    }).hide();

    $('#fileInputContain').on('click', 'button[name=remove]', function () {
        $(this).parents('tr').remove();
        if ($('#fileInputContain').find('tr').length == 0) $('#addInput').click();
    });

    $('#fileInputContain').on('change', 'input[type=file]', function (a, b) {
        if ($(this).attr('hasChanged')) return;
        $('#addInput').click();
        $(this).attr('hasChanged', true);
    });

    $('#addInput').click();

    //lazy load
    $('#attachmentContainer img').lazyload({
        effect: "fadeIn"
    });

    //gallery
    $('#attachmentContainer').magnificPopup({
        delegate: 'a.img-gallry',
        type: 'image',
        closeOnContentClick: false,
        closeBtnInside: false,
        mainClass: 'mfp-with-zoom mfp-img-mobile',
        image: {
            verticalFit: true,
            titleSrc: function (item) {
                return item.el.attr('title') + "<div>" + item.el.next('.img-operation').html() + "</div>";
            }
        },
        gallery: {
            enabled: true
        },
        zoom: {
            enabled: true,
            duration: 300, // don't foget to change the duration also in CSS
            opener: function (element) {
                return element.find('img');
            }
        },
        error: function () {
            console.log('aaa');
        },
        callbacks: {
            updateStatus: function (data) {
                if (data.status == "error") {
                    data.text = data.text + "<br/><div>" + this.currItem.el.next('.img-operation').html() + "</div>";
                }
            }
        }
    });
});


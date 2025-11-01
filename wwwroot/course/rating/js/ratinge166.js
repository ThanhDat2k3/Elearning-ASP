var HMRateCount = HMRateCount || {};
HMRateCount.rate = function () {
    var rateCountBox = $('#course-rate-count');
    var courseId = rateCountBox.data('course');


    $.post('../../course/rating/get-count.html', {courseid:courseId}, function(response) {
        if (response.status == 'success') {
            var html = HMRateCount.getBoxHtml(response);
            rateCountBox.html(html);
            rateCountBox.find('.average-star').each(function (index) {
                var score = parseFloat($(this).data('score'));
                $(this).rateYo({
                    rating: score,
                    readOnly: true
                });
            });
        }
    }, 'JSON');
};

HMRateCount.getBoxHtml = function(data) {
    var html = '<div class="course-rate-container"><div class="average-rate">' +
            '<div class="title">' +
                '<p>Nhận xét</p>' +
                '<p>Đánh giá trung bình</p>' +
            '</div>' +
            '<div class="average-score">'+ data.score +'</div>' +
            '<div class="average-star" data-score="'+ data.score +'"></div>'+
            '</div>' +
        '<div class="detail-rate">' +
            '<div class="rate-title"><p>Chi tiết</p></div>' +
            '<div class="crc-title">' +
                '<ul>';
    for (var i in data.detail) {
        html += '<li>' +
            '<span class="name">'+ data.detail[i].name +'</span>' +
            '<div class="progress">'+
                '<div class="progress-bar" role="progressbar" aria-valuenow="'+ data.detail[i].percent +'" aria-valuemin="0" aria-valuemax="100" style="width: '+ data.detail[i].percent +'%;">'+
                '</div>' +
            '</div>' +
            '<span class="count">'+ data.detail[i].percent +'</span>' +
            '</li>';
    }


    html += '</ul>';
    html += '</div>';
    html += '</div></div>';

    return html;
};

HMRateCount.init = function () {
    if (typeof $.fn.rateYo !== 'undefined') {
        HMRateCount.rate();
    } else {
        var checkElement = $('#hm-raty-test');
        if (checkElement.length) {
            setTimeout(function () {
                HMRateCount.init();
            }, 100);
        } else {
            $('body').append('<div id="hm-raty-test" style="display: none !important;"></div>');
            // load javascript lib
            $.ajax({
                url: "/course/rating/js/jquery.rateyo.js",
                dataType: "script",
                cache: true
            }).done(function () {
                HMRateCount.rate();
            });
        }
    }
};

$(function () {
    HMRateCount.init();
});
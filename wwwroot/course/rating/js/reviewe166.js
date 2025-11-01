var HMReview = HMReview || {};
HMReview.page = 0;
HMReview.perPage = 5;

HMReview.rate = function () {
    var reviewBox = $('#course-review');
    var courseId = reviewBox.data('course');
    var reviewBoxHtml = HMReview.getReviewBoxHtml();
    reviewBox.html(reviewBoxHtml);
    HMReview.getReview(courseId);

    reviewBox.on('click', '.rvl-more', function (e) {
        e.preventDefault();
        HMReview.getReview(courseId);
    });
};

HMReview.getReview = function(courseId) {
    var reviewBox = $('#course-review');
    var reviewContainer = reviewBox.find('.reviews-content');
    var loading = reviewBox.find('.rv-loading');
    loading.show();

    $.post('../../course/rating/get-review.html', {
        courseid: courseId,
        page: HMReview.page,
        perpage: HMReview.perPage
    }, function (response) {
        loading.hide();

        if (response.status == 'success') {
            var html = HMReview.getReviewsItemHtml(response.data);
            reviewContainer.append(html);

            HMReview.page += 1;
            if (response.count < HMReview.perPage) {
                reviewBox.find('.rv-load').hide();
            }

            reviewContainer.find('.rvi-star').each(function (index) {
                var score = parseFloat($(this).data('score'));
                $(this).rateYo({
                    rating: score,
                    readOnly: true,
                    starWidth: "20px"
                });
            });
        }
    }, 'JSON');
};

HMReview.getReviewBoxHtml = function () {
    return '<div class="reviews-container">' +
        '<div class="reviews-content"></div>' +
        '<div class="rv-loading" style="display: none"><img src="/course/rating/images/preloader_stick.gif"></div>' +
        '<div class="rv-load">' +
        '<a href="#" class="rvl-more">Hiển thị thêm đánh giá</a>' +
        '</div>' +
        '</div>';
};

HMReview.getReviewsItemHtml = function (reviews) {
    var html = '';
    for (var i in reviews) {
        html += '<div class="review-item">' +
        '<div class="rvi-user">' +
        '<div class="rvi-image">'+ reviews[i].image +'</div>' +
        '<div class="rvi-name">'+ reviews[i].name +'</div>' +
        '</div>' +
        '<div class="rvi-content">' +
        '<div class="rvic-head"><div class="rvi-star" data-score="'+ reviews[i].score +'"></div><span>'+ reviews[i].time +'</span></div>' +
        //'<div class="rvic-title">'+ reviews[i].title +'</div>' +
        '<div class="rvic-review">'+ reviews[i].content +'</div>' +
        '</div>' +
        '</div>';
    }
    return html;
};

HMReview.init = function () {

    if (typeof $.fn.rateYo !== 'undefined') {
        HMReview.rate();
    } else {
        var checkElement = $('#hm-raty-test');

        if (checkElement.length) {
            setTimeout(function () {
                HMReview.init();
            }, 100);
        } else {
            $('body').append('<div id="hm-raty-test" style="display: none !important;"></div>');
            // load javascript lib
            $.ajax({
                url: "/course/rating/js/jquery.rateyo.js",
                dataType: "script",
                cache: true
            }).done(function () {
                HMReview.rate();
            });
        }
    }
};

$(function () {
    HMReview.init();
});
function shareFb(link) {
    if (link === undefined) {
        window.open(window.location.href, '', 'menubar=no, toolbar=no, resizable=yes, scrollbars=yes, height=600, width=600');
    } else {
        window.open(link, '', 'menubar=no, toolbar=no, resizable=yes, scrollbars=yes, height=600, width=600');
    }
}

$('.mega-menu .have-childs').hover(function () {
    let color = $(this).attr('color');
    $(this).children('a').css('color',color);
    $(this).children('.menu-after-dom').css('border-right','8px solid '+color);
    $(this).find('.menu').css('border-left','solid 4px '+color);
    $(this).find('li.title>a').css('color',color);
});

$('.mega-menu .have-childs').mouseleave(function () {
    $(this).children('a').css('color','#444');
});

$('.tab-list>.tab.active').each(function () {
    let color = $(this).closest('.have-childs').attr('color');
    $(this).css('background-color',color);
    $(this).find('.tab-before-dom').css('background-color',color);
});

$('.home-menu .tab').click(function () {
    let color = $(this).closest('.have-childs').attr('color');
    let dataTab = $(this).data('tab');
    $(this).closest('.menu').find('.tab .tab-before-dom').css('background-color','');
    $(this).closest('.menu').find('.tab').css('background-color','');
    $(this).closest('.menu').find(`.tab[data-tab="${dataTab}"]>.tab-before-dom`).css('background-color',color);
    $(this).closest('.menu').find(`.tab[data-tab="${dataTab}"]`).css('background-color',color);
    
    $(this).closest('.menu').find('.tab').removeClass('active');
    $(this).closest('.menu').find(`.tab[data-tab="${dataTab}"]`).addClass('active');
    
    $(this).closest('.menu').find('.course-list').removeClass('active');
    $(this).closest('.menu').find('div.title').css('background-color',color);
    $(this).closest('.menu').find(`.course-list[data-tab="${dataTab}"]`).addClass('active');
});


$(document).ready(function () {

    $(function() {
        $('.lazy').Lazy({
            scrollDirection: 'vertical',
            effect: 'fadeIn',
            visibleOnly: true,
            onError: function(element) {
                console.log('error loading ' + element.data('src'));
            }
        });
    });
    
    var notifPage = 0;
    getNotification();
    function getNotification() {
        $.ajax({
            url: urlBase + 'hocmai2020/api/getNotification',
            data: {notif_page: notifPage},
            type: 'POST',
            success: function (data) {
                if (data.code == 1) {
                    for(var i = 0; i < Object.keys(data.data[0]).length; i++){
                        if(data.data[0][i].status == 1 || data.data[0][i].status == 2){
                            $('.sub.noti-box ul').append('<li data-id="'+data.data[0][i].id+'" class="no-read noti-title"><a target="_blank" href="'+data.data[0][i].url+'">'+ data.data[0][i].action +'</a></li>')
                        }else{
                            $('.sub.noti-box ul').append('<li data-id="'+data.data[0][i].id+'" class="noti-title"><a target="_blank" href="'+data.data[0][i].url+'">'+ data.data[0][i].action +'</a></li>')
                        }
                    }
                    
                    if( parseInt(data.data[1]) !== 0 ){
                        $('.have-notifi').html(data.data[1]);
                        $('.have-notifi').removeClass('hide');
                    }else{
                        $('.have-notifi').addClass('hide');
                    }
                }
                if (data.code == 0) {
                    if(notifPage == 0){
                        $('.sub.noti-box ul').css({'text-align':'center','padding':'100px 10px'});
                        $('.sub.noti-box ul').append('Bạn chưa có thông báo nào.')
                    }
                    $('.notifi-readmore').css('display','none');
                    
                }
                notifPage++;
            }
        });
    }
    
    $('button.notifi-readmore').click(function () {
        getNotification();
    });
    
    $( ".noti-box" ).delegate( ".noti-title", "click", function() {
        var idnoti = $(this).data('id');
        $.ajax({
            url: urlBase + 'hocmai2020/api/updateNotification',
            data: {idnoti: idnoti},
            type: 'POST'
        });
    });    
    $('a[href*="#"]:not([href="#"])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);

            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html, body').animate({
                    scrollTop: target.offset().top - 30
                }, 500);
                return false;
            }
        }
    });
    
    if ($('#icon-up').length) {
        var scrollTrigger = 200, 
            backToTop = function () {
                var scrollTop = $(window).scrollTop();
                if (scrollTop > scrollTrigger) {
                    $('#icon-up').css('display', 'flex');
                } else {
                    $('#icon-up').css('display', 'none');
                }
            };
        backToTop();
        $(window).on('scroll', function () {
            backToTop();
        });
        $('#icon-up').on('click', function (e) {
            e.preventDefault();
            $('html,body').animate({
                scrollTop: 0
            }, 700);
        });
    }
    
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        if (scroll <= 500) {
            $('.bar-c1').removeClass('active');
        }
        if (scroll >= 500) {
            $('.bar-c1').addClass('active');
            $('.bar-c2').removeClass('active');
            $('.bar-c3').removeClass('active');
        }
        if (scroll >= 1200) {
            $('.bar-c2').addClass('active');
            $('.bar-c1').removeClass('active');
            $('.bar-c3').removeClass('active');
        }
        if (scroll >= 1800) {
            $('.bar-c3').addClass('active');
            $('.bar-c1').removeClass('active');
            $('.bar-c2').removeClass('active');
        }
    });
    
    $(".mega-menu li.have-childs").hover(function () {
        $('.mega-menu li.have-childs').removeClass("active");
        $(this).addClass("active");
        $('.bg-activemenu ').addClass("active");
        $('.wrap-header .main-menu .home-menu').css('z-index',998);
    });
    $(".mega-menu li.have-childs").mouseleave(function () {
        $('.wrap-header .main-menu .home-menu').css('z-index',1);
        $('.mega-menu li.have-childs').removeClass("active ");
        $('.bg-activemenu ').removeClass("active ");
    });

    $('.wrap-header').delegate('.bg-activemenu','click',function(){
        $('.mega-menu li.have-childs').removeClass("active ");
        $('.bg-activemenu ').removeClass("active ");
    })

    $('.list-bar li a').click(function () {
        $('.list-bar li').removeClass('active');
        $(this).parent().addClass('active');
    });
    
    function formatNumber(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.")
    }
    
    $('.title-statistics').each(function () {
        var $this = $(this);
        var countTo = $this.attr('data-count');
        if(isNaN(countTo)){
            return true;
        }
        $({
            countNum: $this.text()
        }).animate({
            countNum: countTo
        }, {
            duration: 600, 
            easing: 'linear',
            step: function () {
                $this.text(Math.floor(this.countNum));
            },
            complete: function () {
                $this.text(formatNumber(this.countNum));
                if ($this.hasClass("syear")) {
                    $this.text(formatNumber(this.countNum) + ' năm');
                }
            }
        });
    });
    var logImgBg = "";
    
    renderSliderBackground($('.home-header-banner'));
    
})

var sliderBanner = $('.home-header-banner').owlCarousel({
    loop:true,
    margin:0,
    nav:false,
    dots: true,
    lazyLoad:true,
    autoplay:true,
    autoplayTimeout:6000,
    autoplayHoverPause:true,
    autoHeight:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
});

sliderBanner.on('translated.owl.carousel', function(event) {
    renderSliderBackground($(this))
});

sliderBanner.on('initialize.owl.carousel', function(event) {
    renderSliderBackground($(this))
});

function renderSliderBackground(el){
    var canvas = document.getElementById("slide-bg");
    var ctx = canvas.getContext('2d');
    var img = el.find('.owl-item.active img');
    
    
    ctx.beginPath();
    ctx.rect(0, 0, canvas.width, canvas.height);
    ctx.fillStyle = "#fff";
    ctx.fill();
    ctx.filter = 'blur(80px)';
    
    if(/^((?!chrome|android).)*safari/i.test(navigator.userAgent))
    {
        ctx.drawImage(img[0], 0, 0, canvas.width / 6, canvas.height / 6, 0, 0, canvas.width, canvas.height);
    }else{
        ctx.drawImage(img[0], -50, -50, canvas.width + 50, canvas.height + 50);
        ctx.drawImage(img[0], -50, -50, canvas.width + 50, canvas.height + 50);
        ctx.drawImage(img[0], -50, -50, canvas.width + 50, canvas.height + 50);
    }
}


$('.owl-container-feedback').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    dots: true,
    autoplay:true,
    autoplayTimeout:6000,
    autoplayHoverPause:true,
    autoHeight:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
})

$('.swiper-container-new-thpt').owlCarousel({
    loop:true,
    margin:10,
    nav:true,
    navText: ['<img alt="pre" src="assets/front/images/icon-nextleft.png"/>','<img alt="next" src="assets/front/images/icon-nextleft.png"/>'],
    dots: false,
    lazyLoad: true,
    autoplay:true,
    autoplayTimeout:4000,
    autoplayHoverPause:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:4
        },
        1000:{
            items:3
        }
    }
})

$('.swiper-container-hots-thpt').owlCarousel({
    loop:true,
    margin:10,
    dots: false,
    nav: true,
    lazyLoad: true,
    navText: ['<img alt="pre" src="assets/front/images/icon-nextleft.png"/>','<img alt="next" src="assets/front/images/icon-nextleft.png"/>'],
    autoplay:false,
    responsive:{
        0:{
            items:2
        },
        600:{
            items:4
        },
        1000:{
            items:5
        }
    }
})


let menuItem = $('.menu .icon').closest('.row');
for(let i =0; i < menuItem.length; i++){
    if(!$(menuItem[i]).hasClass('re-render')){
        let countItem = $(menuItem[i]).find('.items');
        let itemPre = 0;
        switch (countItem.length) {
            case 4: itemPre = 3; break;
            case 3: itemPre = 4; break;
            case 2: itemPre = 6; break;
            case 1: itemPre = 12; break;
            default: itemPre = -1; break;
        }
        
        if(itemPre != -1){
            for(let j =0; j < countItem.length; j++){
                $(countItem[j]).removeClass('col-4');
                $(countItem[j]).addClass('col-'+itemPre);
            }
        }
       
        $(menuItem[i]).addClass('re-render');
    }
}


if($(window).scrollTop() > 1){
    $('.header-top .logo.box-img').addClass('fixed');
    $('.top-header .left-top-header').addClass('fixed');
    $('.header-top .top-header').css('z-index',999);
}

$(window).scroll(function(){
    if($(this).scrollTop() > 1){
        $('.header-top .logo.box-img').addClass('fixed');
        $('.top-header .left-top-header').addClass('fixed');
        $('.header-top .top-header').css('z-index',999);
    }else{
        $('.header-top .logo.box-img').removeClass('fixed');
        $('.top-header .left-top-header').removeClass('fixed');
        $('.header-top .top-header').css('z-index',5);
    }
});
$('#marquee-vertical').marquee({delay: 60000, timing: 20});
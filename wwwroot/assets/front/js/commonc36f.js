var HMClient = {
	facebook: {
		appId: '600873143331099',
		scope: 'email,public_profile'
	},
	google: {
		clientId: '880785099124-b2mb58bvfppe88kmq2ir8omld7g122c6.apps.googleusercontent.com',
		scope: 'profile email'
	},
	yahoo: {
		clientId: 'dj0yJmk9alFreDNnUFVyYkhiJmQ9WVdrOU0zRnVVemxwTjJzbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD0zYg--'
	}
};


if(!$.cookie("time_join_home_new")){
	var time = new Date().getTime();
	$.cookie("time_join_home_new",time,{ expires: 999, path: '/'})
}

$('#feedback-long-form').on('hide.bs.modal', function (e) {
	window.location.replace('index0789.html?themeNew=old');
})

$('.feedback_home_form').submit(function(e){
	e.preventDefault()
	
	
	var data = {
		userInfo : {
			USER_ID: USER_ID,
			CLIENT_IP: CLIENT_IP,
			HTTP_USER_AGENT: HTTP_USER_AGENT,
			HTTP_REFERER: HTTP_REFERER,
			TIME_JOIN: $.cookie("time_join_home_new"),
			TIME_SEND: new Date().getTime(),
			SCREEN_SIZE: screen.height + 'x' + screen.width,
			DOCUMENT_SIZE: $(window).height() + 'x' + $(window).width(),
			
		},
		feedBack: {
			feedbackOption: [],
			feedbackMore: ''
		}
	}
	
	var checkForm = false;
	
	$(this).find('input').each(function(){
		if($(this).is(":checked")){
			checkForm = true;
			data.feedBack.feedbackOption.push($(this).parent('.custom-control').find('span').html())
		}
	})
	
	data.feedBack.feedbackMore = $(this).find('#more-feedback-long').val()

	if(data.feedBack.feedbackMore.trim() != ''){
		checkForm = true;
	}
	
	if(checkForm){
		$.ajax({
			url: 'hocmai2020/api/feedback',
			data: {
				action: 'save',
				data: data
			},
			type: 'POST',
			success: function () {
				$.ajax({
					url: 'hocmai2020/api/feedback',
					data: {
						action: 'save',
						data: data
					},
					type: 'POST',
					success: function () {
						sendcountback();
					},
					error: function () {
						sendcountback();
					}
				});
				
			},
			error: function () {
				$('#feedback-long-form').modal('hide');
			}
		});
	}else{
		$('.feedback_home_form .alert-form').removeClass('hide');
	}
})

function sendcountback(){
	$.ajax({
		url: 'hocmai2020/api/countswitch?a=old',
		type: 'GET',
		success: function () {
			$('#feedback-long-form').modal('hide');
		},
		error: function () {
			$('#feedback-long-form').modal('hide');
		}
	});
}

$('.btn-switch-theme a').click(function(e){
	e.preventDefault()
	$('#feedback-long-form').modal('show');
})
$('.btn-dowload a').click(function(e){
	e.preventDefault()
	$('#feedback-short-form').modal('show');
})


$(document).ready(function() {
	if (new Date().getTime() < new Date('2022-05-12 00:00:00').getTime()) {
		var timeOut = 5000;
		var getUserCountInterval = function() {
			var timeOut = Math.floor(Math.random() * 6000) + 4000;
			// console.log(timeOut);
			getUserCount();
			setTimeout(getUserCountInterval, timeOut);
		}
		setTimeout(getUserCountInterval, timeOut);

	}
    function getUserCount() {
        $.ajax({
            url: urlBase + 'hocmai2020/api/userCount',
            type: 'GET',
            success: function (data) {
                if (data.data)
                    $('.user-counter').text(data.data.toString().replace(/\B(?=(\d{3})+(?!\d))/g, "."));
            }
        });
    }
})
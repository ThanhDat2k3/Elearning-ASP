$(document).ready(function(){
    function YouTubeGetID(url){
        var ID = '';
        url = url.replace(/(>|<)/gi,'').split(/(vi\/|v=|\/v\/|youtu\.be\/|\/embed\/)/);
        if(url[2] !== undefined) {
            ID = url[2].split(/[^0-9a-z_\-]/i);
            ID = ID[0];
        }else {
            ID = url;
        }
        return ID;
    }
    
    $('select.select2').select2({
        minimumResultsForSearch: Infinity
    });

    $('#gv-subject-filter, #gv-class-filter').change(function(){
        $('#btnSearchGv').click();
    });
    
    if(typeof $.fn.perfectScrollbar == 'function'){
        $('.pf-testimonial-wrap, .pfs-content-container, .pfc-right-content').perfectScrollbar();
    }

    $('a.pfn-yt').click(function(){
        var $yturl = $(this).attr('href');
        var $ytModal = $('#youtubeModal');
        var $ythtml = '<iframe class="ytembed" width="560" height="315" src="https://www.youtube.com/embed/'+YouTubeGetID($yturl)+'" frameborder="0" allowfullscreen></iframe>';
        $('#ytplayer').empty().html($ythtml);
        $ytModal.modal('show');
        return false;
    });
    
    // **************
    // ScrollAnchor
    // **************
    $('[data-scroll]').on('click', function() {
    	var scrollAnchor = $(this).data('scroll'),
    		scrollPoint = $('[data-anchor="' + scrollAnchor + '"]').show().offset().top;
            
    	$('body,html').animate({
    		scrollTop: scrollPoint
    	}, 500);
    	return false;
    });

    var $page = getUrlParam('page');
    if ($page) {
        $('[data-scroll="main"]').trigger('click');
    }
});
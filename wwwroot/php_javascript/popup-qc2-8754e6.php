        (function(h,o,t,j,a,r){
            h.hj=h.hj||function(){(h.hj.q=h.hj.q||[]).push(arguments)};
            h._hjSettings={hjid:239700,hjsv:6};
            a=o.getElementsByTagName('head')[0];
            r=o.createElement('script');r.async=1;
            r.src=t+h._hjSettings.hjid+j+h._hjSettings.hjsv;
            a.appendChild(r);
        })(window,document,'https://static.hotjar.com/c/hotjar-','.js?sv=');
            $(document).ready(function () {
        $("body").append(`
        <div id="popup-popupHMO200" class="modal hocmai-modal fade" role="dialog">
            <div class="modal-dialog" style="transform: translate(-50%, -50%);top: 50%;left: 50%;margin: 0;">
                <div class="modal-content" style="background-color: unset;border: unset;box-shadow: unset;max-width: 470px;">
                    <button style="position: absolute;right:10px;top: 7px;opacity: 1" type="button" 
                        class="close popup-close" data-dismiss="modal">&times;</button>
                    <a target="_blank" href="https://hocmai.link/WHMpopupt10-0810">
                        <img src="https://hocmai.vn/php_javascript/images/popup-2025/popup_hmo_200.png" style="max-width: 100%;">
                    </a>
                </div>
            </div>
        </div>
        `);

        var $showPopupCountpopupHMO200 = $.cookie("user_hocmai_popup-popupHMO200-count");
        if (typeof $showPopupCountpopupHMO200 == 'undefined') {
            $showPopupCountpopupHMO200 = 0;
        }
        if( $showPopupCountpopupHMO200 < 1 ){
            $showPopupCountpopupHMO200++;
            $.cookie("user_hocmai_popup-popupHMO200-count", $showPopupCountpopupHMO200, { expires: null, path: '/'});
            $('#popup-popupHMO200').modal('show');
        }
    });
    
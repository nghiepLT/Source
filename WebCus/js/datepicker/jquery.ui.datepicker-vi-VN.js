jQuery(function ($) {
    $.datepicker.regional["vi-VN"] =
	{
	    closeText: "Đóng",
	    prevText: "Trước",
	    nextText: "Sau",
	    currentText: "Hôm nay",
	    monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
	    monthNamesShort: ["Thg 1", "Thg 2", "Thg 3", "Thg 4", "Thg 5", "Thg 6", "Thg 7", "Thg 8", "Thg 9", "Thg 10", "Thg 11", "Thg 12"],
	    dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
	    dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
	    dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
	    weekHeader: "Tuần",
	    dateFormat: "dd/mm/yy",
	    firstDay: 1,
	    isRTL: false,
	    showMonthAfterYear: false,
	    yearSuffix: ""
	};

    $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
});
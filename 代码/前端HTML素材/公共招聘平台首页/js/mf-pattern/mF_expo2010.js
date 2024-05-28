myFocus.pattern.extend({
	'mF_expo2010': function(settings, $) {
		var $focus = $(settings);
		var $picList = $focus.find('.pic li');
		var $txtList = $focus.addListTxt().find('li');
		var $numList = $focus.addListNum().find('li');
		$focus.addHtml('<div class="txt_bg"></div>');
		var txtH = settings.txtHeight;
		$picList.each(function(i) {
			this.style.display = 'none';
			$txtList[i].style.bottom = -txtH + 'px';
		});
		$focus.play(function(prev) {
			$picList.eq(prev).fadeOut();
			$numList[prev].className = '';
		}, function(next, n, prev) {
			$picList.eq(next).fadeIn();
			$txtList.eq(prev).slide({
				bottom: -txtH
			}, 200, 'swing', function() {
				$txtList.eq(next).slide({
					bottom: 0
				}, 200);
			});
			$numList[next].className = 'current';
		});
		settings.delay = 200;
		$focus.bindControl($numList);
	}
});
myFocus.config.extend({
	'mF_expo2010': {
		txtHeight: 36
	}
});
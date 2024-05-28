myFocus.pattern.extend({
	'mF_fancy': function(settings, $) {
		var $focus = $(settings);
		var $txtList = $focus.addListTxt().find('li');
		var $numList = $focus.addListNum().find('li'),
			n = $numList.length;
		var $thumbList = $focus.addListThumb().find('li');
		var $picUls = $focus.find('.pic ul').repeat(n);
		var $prevBtn = $focus.addHtml('<div class="prev"><a href="javascript:;">&#8249;</a></div>');
		var $nextBtn = $focus.addHtml('<div class="next"><a href="javascript:;">&#8250;</a></div>');
		var $picListArr = [];
		var w = Math.round(settings.width / n);
		$picUls.each(function(i) {
			$(this).css({
				width: w * (i + 1),
				zIndex: n - i
			});
			$picListArr.push($(this).find('li'));
		});
		var numW = Math.round((settings.width - n + 1) / n);
		$numList.each(function(i) {
			this.style.width = numW + 'px';
			if(i === (n - 1)) this.style.width = numW + 1 + 'px';
			$thumbList.eq(i)[0].style.width = w + 'px';
		});
		$focus[0].style.height = settings.height + 12 + 'px';
		var params = {
				isRunning: false
			},
			done = 0;
		var eff = {
			1: 'surf',
			2: 'sliceUpDown',
			3: 'fold',
			4: 'sliceDown',
			5: 'fade',
			6: settings.effect
		};
		var effIndex = eff[6] === 'random' ? 0 : 6;
		$focus.play(function(i) {
			params.isRunning = true;
			$numList[i].className = '';
			$txtList[i].className = '';
		}, function(i) {
			$numList[i].className = 'current';
			$txtList[i].className = 'current';
			var r = effIndex || (1 + Math.round(Math.random() * 4));
			for(var j = 0; j < n; j++) effect[eff[r]]($picListArr[j], j, i);
		});
		var effect = {
			sliceDown: function($picList, j, i) {
				var css = {
						zIndex: 1,
						display: 'block',
						height: 0
					},
					param = {
						height: settings.height
					},
					animTime = (j + 1) * 100 + 400,
					t = 50;
				this.fx($picList, j, i, css, param, animTime, t);
			},
			sliceUpDown: function($picList, j, i) {
				var isSin = j % 2;
				var css = {
						zIndex: 1,
						display: 'block',
						height: 0,
						bottom: isSin ? 'auto' : 0,
						top: isSin ? 0 : 'auto'
					},
					param = {
						height: settings.height
					},
					animTime = 500,
					t = 50;
				this.fx($picList, j, i, css, param, animTime, t);
			},
			fold: function($picList, j, i) {
				var css = {
						zIndex: 1,
						display: 'block',
						width: w * j
					},
					param = {
						width: w * (j + 1)
					},
					animTime = 600,
					t = 50;
				this.fx($picList, j, i, css, param, animTime, t);
			},
			surf: function($picList, j, i) {
				var css = {
						zIndex: 1,
						display: 'block',
						left: -settings.width + j * w
					},
					param = {
						left: 0
					},
					animTime = (j + 1) * 50 + 300,
					t = 50;
				this.fx($picList, j, i, css, param, animTime, t);
			},
			fx: function($picList, j, i, css, param, animTime, t) {
				setTimeout(function() {
					$picList.eq(i).css(css).slide(param, animTime, function() {
						$picList.each(function() {
							this.style.display = 'none'
						});
						this.style.cssText = 'z-index:"";display:block';
						done += 1;
						if(done === n) params.isRunning = false, done = 0;
					});
				}, j * t);
			},
			fade: function($picList, j, i) {
				setTimeout(function() {
					$picList.eq(i).css({
						zIndex: 1,
						display: 'block'
					}).fadeIn(function() {
						$picList.each(function() {
							this.style.display = 'none'
						});
						this.style.cssText = 'z-index:"";display:block';
						done += 1;
						if(done === n) params.isRunning = false, done = 0;
					});
				}, j * 100);
			}
		}
		settings.trigger = 'click';
		$focus.bindControl($numList, params);
		$focus.bindControl($thumbList, params);
		$prevBtn.bind('click', function() {
			if(!params.isRunning) $focus.run('-=1')
		});
		$nextBtn.bind('click', function() {
			if(!params.isRunning) $focus.run('+=1')
		});
		$focus.bind('mouseover', function() {
			$prevBtn.addClass('show'), $nextBtn.addClass('show')
		});
		$focus.bind('mouseout', function() {
			$prevBtn.removeClass('show'), $nextBtn.removeClass('show')
		});
		var onthumb = false;
		$numList.each(function(i) {
			$(this).bind('mouseover', function() {
				if(this.className.indexOf('current') !== -1) return;
				$thumbList.eq(i).css({
					visibility: 'visible'
				}).slide({
					top: 28
				}, 400).fadeIn(400, 'easeOut');
			});
			$(this).bind('mouseout', function() {
				setTimeout(function() {
					if(!onthumb) $thumbList.eq(i).slide({
						top: 0
					}, 400).fadeOut(400, 'easeOut', function() {
						this.style.visibility = 'hidden'
					});
				}, 0);
			});
		});
		$thumbList.each(function(i) {
			$(this).bind('mouseover', function() {
				onthumb = true;
			});
			$(this).bind('mouseout', function() {
				onthumb = false;
				$thumbList.eq(i).slide({
					top: 0
				}, 400).fadeOut(400, 'easeOut', function() {
					this.style.visibility = 'hidden'
				});
			});
		});
	}
});
myFocus.config.extend({
	'mF_fancy': {
		effect: 'random'
	}
});
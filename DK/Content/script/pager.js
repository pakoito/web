﻿(function(d){d.each(["backgroundColor","borderBottomColor","borderLeftColor","borderRightColor","borderTopColor","color","outlineColor"],function(f,e){d.fx.step[e]=function(g){if(!g.colorInit){g.start=c(g.elem,e);g.end=b(g.end);g.colorInit=true}g.elem.style[e]="rgb("+[Math.max(Math.min(parseInt((g.pos*(g.end[0]-g.start[0]))+g.start[0]),255),0),Math.max(Math.min(parseInt((g.pos*(g.end[1]-g.start[1]))+g.start[1]),255),0),Math.max(Math.min(parseInt((g.pos*(g.end[2]-g.start[2]))+g.start[2]),255),0)].join(",")+")"}});function b(f){var e;if(f&&f.constructor==Array&&f.length==3){return f}if(e=/rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(f)){return[parseInt(e[1]),parseInt(e[2]),parseInt(e[3])]}if(e=/rgb\(\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*\)/.exec(f)){return[parseFloat(e[1])*2.55,parseFloat(e[2])*2.55,parseFloat(e[3])*2.55]}if(e=/#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(f)){return[parseInt(e[1],16),parseInt(e[2],16),parseInt(e[3],16)]}if(e=/#([a-fA-F0-9])([a-fA-F0-9])([a-fA-F0-9])/.exec(f)){return[parseInt(e[1]+e[1],16),parseInt(e[2]+e[2],16),parseInt(e[3]+e[3],16)]}if(e=/rgba\(0, 0, 0, 0\)/.exec(f)){return a.transparent}return a[d.trim(f).toLowerCase()]}function c(g,e){var f;do{f=d.curCSS(g,e);if(f!=""&&f!="transparent"||d.nodeName(g,"body")){break}e="backgroundColor"}while(g=g.parentNode);return b(f)}var a={aqua:[0,255,255],azure:[240,255,255],beige:[245,245,220],black:[0,0,0],blue:[0,0,255],brown:[165,42,42],cyan:[0,255,255],darkblue:[0,0,139],darkcyan:[0,139,139],darkgrey:[169,169,169],darkgreen:[0,100,0],darkkhaki:[189,183,107],darkmagenta:[139,0,139],darkolivegreen:[85,107,47],darkorange:[255,140,0],darkorchid:[153,50,204],darkred:[139,0,0],darksalmon:[233,150,122],darkviolet:[148,0,211],fuchsia:[255,0,255],gold:[255,215,0],green:[0,128,0],indigo:[75,0,130],khaki:[240,230,140],lightblue:[173,216,230],lightcyan:[224,255,255],lightgreen:[144,238,144],lightgrey:[211,211,211],lightpink:[255,182,193],lightyellow:[255,255,224],lime:[0,255,0],magenta:[255,0,255],maroon:[128,0,0],navy:[0,0,128],olive:[128,128,0],orange:[255,165,0],pink:[255,192,203],purple:[128,0,128],violet:[128,0,128],red:[255,0,0],silver:[192,192,192],white:[255,255,255],yellow:[255,255,0],transparent:[255,255,255]}})(jQuery);
var pager = 
{
	firstItem : -1
	,
	totalItems : -1
	,
	lastItem : -1
	,
	tempText : null
	,
	enabled : false
	,
	postUrl : null
	,
	postUrlToId : null
	,
	init : function(postUrl,postUrlToId)
	{
		if ($(".firstItem").length > 0)
		{
			pager.firstItem = parseInt($(".firstItem").text(), 10);
			pager.lastItem = parseInt($(".lastItem").text(), 10);
			pager.totalItems = parseInt($(".totalItems").text(), 10);
			pager.enabled = true;
			
			pager.postUrl = postUrl;
			pager.postUrlToId = postUrlToId;
			if (pager.firstItem == 1)
			{
				$("div.pager").hide();
				pager.showCurrentStatus();
				if (window.location.hash)
				{
					pager.navigateToId();
				}
			}
		}
		$(document).trigger("dataLoaded");
	}
	,
	loadingStart : function()
	{
		//loading
		pager.enabled = false;
		$("#pagerClient").toggleClass("loading");
	}
	,
	loadingEnd : function()
	{
		$("#pagerClient").toggleClass("loading");
		pager.enabled = true;
	}
	,
	showCurrentStatus : function()
	{
		$(".lastItem").text(pager.lastItem.toString());
		if (pager.lastItem == pager.totalItems)
		{
			//TODO: Don't hide it, just disable it
			$("#pagerClient").stop().hide();
			$("div.toolbar li.showAll").hide();
		}
		else
		{
			$("#pagerClient").slideDown();
			$("div.toolbar li.showAll").show();
		}
	}
	,
	more : function()
	{
		if (pager.enabled && pager.lastItem < pager.totalItems)
		{
			var fromId = $("#messages > li:last").attr("id").substring(3);
			$.post(pager.postUrl, {from:fromId, initIndex:$("#messages > li").length}, pager.moreCallback);
			pager.loadingStart();
		}
	}
	,
	moreCallback : function(htmlText)
	{
		$("#messages").append(htmlText);
		pager.lastItem = $("#messages > li").length;
		pager.loadingEnd();
		pager.showCurrentStatus();
		$(document).trigger("dataLoaded");
	}
	,
	navigateToId : function()
	{
		if (pager.enabled && $(window.location.hash).length == 0)
		{
			//Load messages until that id
			if (window.location.hash.indexOf("msg") == 1)
			{
				pager.loadingStart();
				var lastMsg = window.location.hash.substring(4);
				var firstMsg = $("#messages > li:last").attr("id").substring(3);
				$.post(pager.postUrlToId, {firstMsg:firstMsg,lastMsg:lastMsg,initIndex:$("#messages > li").length}, pager.navigateToIdCallback);
			}
		}
		else if ($(window.location.hash).length == 1)
		{
			pager.highlightMessage(window.location.hash);
		}
	}
	,
	navigateToIdCallback : function(htmlText)
	{
		$("#messages").append(htmlText);
		pager.lastItem = $("#messages > li").length;
		$("#messages > li:last").addClass("highlight");
		var temp = window.location.hash;
		window.location.hash = "#";
		window.location.hash = temp;
		pager.loadingEnd();
		pager.showCurrentStatus();
		$(document).trigger("dataLoaded");
		pager.highlightMessage(window.location.hash);
	}
	,
	highlightMessage : function(selector)
	{
		var original = $(selector).css("backgroundColor");
		$(selector).animate({ 
		backgroundColor: "#DCECF4"
		}, 2000, "linear", function(){
			$(selector).animate({backgroundColor: original}, 2000, "linear", function(){$(selector).css("backgroundColor", null)});
		});
	}
	,
	showAll : function()
	{
		if (pager.enabled)
		{
			pager.loadingStart();
			var lastMsg = 1000;
			var firstMsg = $("#messages > li:last").attr("id").substring(3);
			$.post(pager.postUrlToId, {firstMsg:firstMsg,lastMsg:lastMsg,initIndex:$("#messages > li").length}, pager.showAllCallback);
		}
		return false;
	}
	,
	showAllCallback : function(htmlText)
	{
		$("#messages").append(htmlText);
		pager.lastItem = $("#messages > li").length;
		pager.loadingEnd();
		pager.showCurrentStatus();
		$(document).trigger("dataLoaded");
	}
}	
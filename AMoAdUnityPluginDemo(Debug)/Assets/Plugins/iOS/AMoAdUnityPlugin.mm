//
//  AMoAdUnityPlugin.mm
//
//  Copyright (c) 2015年 AMoAd inc. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "AMoAdView.h"
#import "AMoAdInterstitial.h"

#if UNITY_VERSION <= 434
#import "iPhone_View.h" // UnityGetGLViewController
#endif


#pragma mark - Externs

#ifdef __cplusplus
extern "C" {
#endif

  void amoad_show(const char *cSid,
                     int hAlign,
                     int vAlign,
                     int adjustMode,
                     int rotateTrans,
                     int clickTrans,
                     const char *cImageName,
                     int x,
                     int y,
                     int timeoutMillis
                     );
  void amoad_hide(const char *cSid);
  void amoad_dispose(const char *cSid);

  void amoad_prepare_interstitial(const char *cSid, int timeoutMillis);
  void amoad_set_interstitial_display_clickable(const char *cSid, bool *cClickable);
  void amoad_set_interstitial_dialog_shown(const char *cSid, bool *cShown);
  void amoad_set_interstitial_portrait_panel(const char *cSid, const char *cImageName);
  void amoad_set_interstitial_landscape_panel(const char *cSid, const char *cImageName);
  void amoad_set_interstitial_link_button(const char *cSid, const char *cImageName, const char *cHighlighted);
  void amoad_set_interstitial_close_button(const char *cSid, const char *cImageName, const char *cHighlighted);
  void amoad_show_interstitial(const char *cSid);
  void amoad_close_interstitial(const char *cSid);

  void amoad_set_interstitial_auto_reload(const char *cSid, bool autoReload);
  void amoad_load_interstitial(const char *cSid);
  bool amoad_is_interstitial_loaded(const char *cSid);

#ifdef __cplusplus
}
#endif


#pragma mark - Utilities

static NSString* string_with_cstring(const char *c) {
  return @(c);
}

static UIViewController* get_view_controller() {
  return UnityGetGLViewController();
}

static AMoAdView *find_amoad_view(NSString *sid) {
  for (id view in get_view_controller().view.subviews) {
    if ([view isKindOfClass:[AMoAdView class]]) {
      AMoAdView *amoadView = (AMoAdView *)view;
      if ([amoadView.sid isEqualToString:sid]) {
        return amoadView;
      }
    }
  }
  return nil;
}


#pragma mark - Implements

void amoad_show(const char *cSid,
                   int hAlign,
                   int vAlign,
                   int adjustMode,
                   int rotateTrans,
                   int clickTrans,
                   const char *cImageName,
                   int x,
                   int y,
                   int timeoutMillis
                   ) {
  NSString *sid = string_with_cstring(cSid);

  AMoAdView *amoadView = find_amoad_view(sid);
  if (amoadView) {
    [amoadView show];
  } else {
    if (cImageName) {
      NSString *imageName = string_with_cstring(cImageName);
      amoadView = [[AMoAdView alloc] initWithImage:[UIImage imageNamed:imageName]
                                        adjustMode:(AMoAdAdjustMode)adjustMode];
    } else {
      amoadView = [[AMoAdView alloc] initWithFrame:CGRectMake((CGFloat)x, (CGFloat)y, 0.0, 0.0)];
      amoadView.adjustMode = (AMoAdAdjustMode)adjustMode;
    }
    if (amoadView) {
      amoadView.horizontalAlign = (AMoAdHorizontalAlign)hAlign;
      amoadView.verticalAlign   = (AMoAdVerticalAlign)vAlign;

      amoadView.networkTimeoutMillis = timeoutMillis;

      amoadView.rotateTransition  = AMoAdRotateTransition(rotateTrans);
      amoadView.clickTransition   = AMoAdClickTransition(clickTrans);

      amoadView.sid = sid;

      [get_view_controller().view addSubview:amoadView];
    }
  }
}

void amoad_hide(const char *cSid) {
  NSString *sid = string_with_cstring(cSid);
  AMoAdView *amoadView = find_amoad_view(sid);
  if (amoadView) {
    [amoadView hide];
  }
}

void amoad_dispose(const char *cSid) {
  NSString *sid = string_with_cstring(cSid);
  AMoAdView *amoadView = find_amoad_view(sid);
  if (amoadView) {
    [amoadView removeFromSuperview];
  }
}


void amoad_prepare_interstitial(const char *cSid, int timeoutMillis) {
  NSString *sid = string_with_cstring(cSid);
  [AMoAdInterstitial registerAdWithSid:sid];
  [AMoAdInterstitial setNetworkTimeoutWithSid:sid millis:timeoutMillis];
}

void amoad_set_interstitial_display_clickable(const char *cSid, bool *cClickable) {
  NSString *sid = string_with_cstring(cSid);
  BOOL clickable = (cClickable) ? YES : NO;
  [AMoAdInterstitial setDisplayWithSid:sid clickable:clickable];
}

void amoad_set_interstitial_dialog_shown(const char *cSid, bool *cShown) {
  NSString *sid = string_with_cstring(cSid);
  BOOL shown = (cShown) ? YES : NO;
  [AMoAdInterstitial setDialogWithSid:sid shown:shown];
}

void amoad_set_interstitial_portrait_panel(const char *cSid, const char *cImageName) {
  NSString *sid = string_with_cstring(cSid);
  if (cImageName) {
    NSString *imageName = string_with_cstring(cImageName);
    [AMoAdInterstitial setPortraitPanelWithSid:sid image:[UIImage imageNamed:imageName]];
  }
}

void amoad_set_interstitial_landscape_panel(const char *cSid, const char *cImageName) {
  NSString *sid = string_with_cstring(cSid);
  if (cImageName) {
    NSString *imageName = string_with_cstring(cImageName);
    [AMoAdInterstitial setLandscapePanelWithSid:sid image:[UIImage imageNamed:imageName]];
  }
}

void amoad_set_interstitial_link_button(const char *cSid, const char *cImageName, const char *cHighlighted) {
  NSString *sid = string_with_cstring(cSid);
  if (cImageName || cHighlighted) {
    NSString *imageName = string_with_cstring(cImageName);
    NSString *highlighted = string_with_cstring(cHighlighted);
    [AMoAdInterstitial setLinkButtonWithSid:sid image:[UIImage imageNamed:imageName] highlighted:[UIImage imageNamed:highlighted]];
  }
}
void amoad_set_interstitial_close_button(const char *cSid, const char *cImageName, const char *cHighlighted) {
  NSString *sid = string_with_cstring(cSid);
  if (cImageName || cHighlighted) {
    NSString *imageName = string_with_cstring(cImageName);
    NSString *highlighted = string_with_cstring(cHighlighted);
    [AMoAdInterstitial setCloseButtonWithSid:sid image:[UIImage imageNamed:imageName] highlighted:[UIImage imageNamed:highlighted]];
  }
}

void amoad_show_interstitial(const char *cSid) {
  NSString *sid = string_with_cstring(cSid);
  [AMoAdInterstitial showAdWithSid:sid completion:nil];
}

void amoad_close_interstitial(const char *cSid) {
  NSString *sid = string_with_cstring(cSid);
  [AMoAdInterstitial closeAdWithSid:sid];
}

void amoad_set_interstitial_auto_reload(const char *cSid, bool autoReload) {
  NSString *sid = string_with_cstring(cSid);
  [AMoAdInterstitial setAutoReloadWithSid:sid autoReload:autoReload];
}

void amoad_load_interstitial(const char *cSid) {
  NSString *sid = string_with_cstring(cSid);
  [AMoAdInterstitial loadAdWithSid:sid completion:nil];
}

bool amoad_is_interstitial_loaded(const char *cSid) {
  NSString *sid = string_with_cstring(cSid);
  return [AMoAdInterstitial isLoadedAdWithSid:sid];
}

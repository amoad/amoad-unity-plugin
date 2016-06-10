//
//  AMoAdUnityPlugin.mm
//
//  Copyright (c) 2015年 AMoAd inc. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "AMoAdNative.h"

#if UNITY_VERSION <= 434
#import "iPhone_View.h" // UnityGetGLViewController
#endif


#pragma mark - Externs

#ifdef __cplusplus
extern "C" {
#endif

  void amoad_native_load(const char *cSid, const char *cTag, int x, int y, int width, int height);
  void amoad_native_remove(const char *cSid, const char *cTag);
  void amoad_native_reload(const char *cSid, const char *cTag);
  void amoad_native_show(const char *cSid, const char *cTag);
  void amoad_native_hide(const char *cSid, const char *cTag);
  void amoad_native_start_rotation(const char *cSid, const char *cTag, int seconds);
  void amoad_native_stop_rotation(const char *cSid, const char *cTag);

  void amoad_native_set_html_url_string(const char *cHtmlUrlString);
  void amoad_native_load_with_option(const char *cSid, const char *cTag, int x, int y, int width, int height, const char *cOption);

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


#pragma mark - Implements

void amoad_native_load(const char *cSid, const char *cTag, int x, int y, int width, int height) {
  amoad_native_load_with_option(cSid, cTag, x, y, width, height, "{}");
}

void amoad_native_remove(const char *cSid, const char *cTag) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  [[AMoAdNative viewWithSid:sid tag:tag] removeFromSuperview];
  [AMoAdNative disposeViewWithSid:sid tag:tag];
}

void amoad_native_reload(const char *cSid, const char *cTag) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  [AMoAdNative reloadWithSid:sid tag:tag];
}

void amoad_native_show(const char *cSid, const char *cTag) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  [AMoAdNative showWithSid:sid tag:tag];
}

void amoad_native_hide(const char *cSid, const char *cTag) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  [AMoAdNative hideWithSid:sid tag:tag];
}

void amoad_native_start_rotation(const char *cSid, const char *cTag, int seconds) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  [AMoAdNative startRotationWithSid:sid tag:tag seconds:seconds];
}

void amoad_native_stop_rotation(const char *cSid, const char *cTag) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  [AMoAdNative stopRotationWithSid:sid tag:tag];
}

void amoad_native_set_html_url_string(const char *cHtmlUrlString) {
  NSString *htmlUrlString = string_with_cstring(cHtmlUrlString);
  [AMoAdNative setHtmlUrlString:htmlUrlString];
}

void amoad_native_load_with_option(const char *cSid, const char *cTag, int x, int y, int width, int height, const char *cOption) {
  NSString *sid = string_with_cstring(cSid);
  NSString *tag = string_with_cstring(cTag);
  CGRect frame = CGRectMake(x, y, width, height);
  NSString *option = string_with_cstring(cOption);
  NSData *data = [option dataUsingEncoding:NSUTF8StringEncoding];
  NSError *error = nil;
  NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&error];
  if (error != nil) {
    dic = @{};
  }
  [AMoAdNative loadWithSid:sid tag:tag frame:frame completion:nil option:dic];
  [AMoAdNative hideWithSid:sid tag:tag];
  [get_view_controller().view addSubview:[AMoAdNative viewWithSid:sid tag:tag]];
}

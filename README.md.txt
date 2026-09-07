# Unity × Live2D × OpenSeeFace Facial Tracking

Unity上のLive2Dモデルを、Webカメラから取得した顔情報を用いてリアルタイムに動かす個人制作です。

## 概要

OpenSeeFaceから取得したフェイストラッキングデータをUnityで受信し、Live2D Cubism SDKを使用してモデルのパラメータへ反映しました。

主に以下の機能を実装しています。

- 顔の上下・左右の回転
- 左右のまばたき
- 口の開閉
- 表情パラメータの制御
- トラッキング感度の調整
- Unity上での設定用UI

## 使用技術

- Unity
- C#
- Live2D Cubism SDK for Unity
- OpenSeeFace
- Webカメラ

## 工夫した点

OpenSeeFaceから取得した値をそのままLive2Dモデルへ反映すると、目を閉じてもモデル側では完全に閉じない問題がありました。

そこで、実際の開眼時・閉眼時の値を確認し、取得値をLive2Dのパラメータ範囲に合わせて変換する処理を調整しました。

また、顔の上下方向が実際の動きと逆になる問題についても、取得値とモデルの挙動を確認しながら補正しました。

## このリポジトリについて

このリポジトリには、自分が実装・調整したC#スクリプトを掲載しています。

Live2D Cubism SDKおよびLive2Dサンプルモデルなどの第三者アセットは含めていません。

## Scripts

- `OpenSee.cs` - OpenSeeFaceとの通信・データ取得
- `FaceRotationTest.cs` - 顔の回転情報の反映
- `ExpressionController.cs` - 表情・まばたき・口の制御
- `SensitivityUI.cs` - トラッキング感度の調整
- `ControlPanelController.cs` - 設定UIの制御

## 今後追加したい機能

- トラッキングの安定性向上
- 表情認識の拡張
- UIの改善
- 設定値の保存・読み込み
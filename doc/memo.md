# メモ

## wwwフォルダ下のファイル構成

```
www/
├── data/
│   ├── Armors.json
│   ├── Items.json
│   ├── System.json
│   └── Weapons.json
└── save/
    ├── common.rpgsave
    └── file1.rpgsave
```

## 各ファイル説明

### Armor.json

```json
[
    null,
    {
        "id": 1,
        "atypeId": 5,
        "description": "",
        "etypeId": 2,
        "traits": [{"code": 22,"dataId": 1,"value": 0}],
        "iconIndex": 128,
        "name": "盾",
        "note": "",
        "params": [0,0,0,10,0,0,0,0],
        "price": 300
    },
]
```

|プロパティ名|説明|
|-|-|
|id|防具ID|
|name|防具名|
|description|説明|

全防具情報のJSON 防具データの配列 配列要素の最初はnull

### Items.json
```json
[
    null,
    {
        "id": 1,
        "animationId": 69,
        "consumable": false,
        "damage": {"critical": false,"elementId": 0,"formula": "0","type": 0,"variance": 20},
        "description": "ドントに売っているおもちゃのモデルガン。\n本物としか思えない精巧な作りをしている。戦闘で使用すると…？",
        "effects": [{"code": 21,"dataId": 1,"value1": 2,"value2": 0}],
        "hitType": 0,
        "iconIndex": 118,
        "itypeId": 2,
        "name": "ロケットランチャー",
        "note": "",
        "occasion": 1,
        "price": 2000,
        "repeats": 1,
        "scope": 2,
        "speed": 0,
        "successRate": 100,
        "tpGain": 0
    },
]
```

|プロパティ名|説明|
|-|-|
|id|アイテムID|
|name|アイテム名|
|description|説明|

全アイテム情報のJSON アイテムデータの配列 配列要素の最初はnull

### System.json

```json
{
    "switches": [
        "",
        "スイッチ1",
        "スイッチ2",
        "スイッチ3",
        "スイッチ4",
        "スイッチ5",
    ],
    "variables": [
        "",
        "変数1",
        "変数2",
        "変数3",
        "変数4",
        "変数5",
    ],
}
```

|プロパティ名|説明|
|-|-|
|switches|スイッチ名の配列 インデックスとスイッチIDが対応している|
|variables|変数名の配列 インデックスと変数IDが対応している|

### Weapons.json

```json
[
    null,
    {
        "id": 1,
        "animationId": 6,
        "description": "",
        "etypeId": 1,
        "traits": [
            {
                "code": 31,
                "dataId": 1,
                "value": 0
            },
            {
                "code": 22,
                "dataId": 0,
                "value": 0
            }
        ],
        "iconIndex": 97,
        "name": "剣",
        "note": "",
        "params": [
            0,
            0,
            10,
            0,
            0,
            0,
            0,
            0
        ],
        "price": 500,
        "wtypeId": 2
    },
]
```

|プロパティ名|説明|
|-|-|
|id|武器ID|
|name|武器名|
|description|説明|

全武器情報のJSON 武器データの配列 配列要素の最初はnull

### common.rpgsave

```json
{
  "gameSwitches": {
    "507": false,
    "514": false,
    "521": false,
    "@c": 2
  },
  "gameVariables": {
    "@c": 3
  },
  "@c": 1
}
```

|プロパティ名|説明|
|-|-|
|gameSwitches|共通スイッチ|
|gameVariables|共通変数|

### file1.rpgsave

```json
{
  "switches": {
    "_data": {
      "@c": 23,
      "@a": [null,null,null,false,false,]
    },
    "@c": 22,
    "@": "Game_Switches"
  },
  "variables": {
    "_data": {
      "@c": 25,
      "@a": [null,0,0,0,0,200,0,]
    },
    "@c": 24,
    "@": "Game_Variables"
  },
  "actors": {
    "_data": {
      "@c": 29,
      "@a": [
        null,
        {
          "_hp": 450,
          "_mp": 1,
          "_tp": 30,
          "_actorId": 1,
          "_name": "坊ちゃま",
          "_level": 1,
          "_exp": {
            "1": 0,
            "@c": 45
          },
        },
      ]
    },
    "@c": 28,
    "@": "Game_Actors"
  },
  "party": {
    "_gold": 0,
    "_items": {"1": 842,"2": 552,"3": 1,"4": 1,"5": 1,"@c": 210},
    "_weapons": {"@c": 211},
    "_armors": {"3": 99,"@c": 212},
    "@c": 207,
    "@": "Game_Party"
  },
}
```

|プロパティ名|説明|
|-|-|
|switches|_data::@aにスイッチの値の配列がある 型はboolかnull インデックスがスイッチIDに対応している|
|variables|_data::@aに変数の値の配列がある 型は任意のobjectかnull インデックスが変数IDに対応している|
|actors|_data::@aにアクターの配列がある|
|party::_gold|所持金|
|party::_items|所持アイテム アイテムIDをキー、所持数を値として持つ|
|party::_weapons|所持武器 武器IDをキー、所持数を値として持つ|
|party::_armors|所持防具 防具IDをキー、所持数を値として持つ|

## IDとインデックス

|||
|-|-|
|システムが持つスイッチ名配列|先頭null 全てのスイッチの名前|
|セーブデータが持つスイッチ値配列|先頭null スイッチの値が記されているが後ろが省略されていることがある|
|共通セーブデータが持つスイッチ値オブジェクト|スイッチIDと値のペアを持つ|

- 全スイッチのIDと名前と値のオブジェクトの配列を作りたい
- システムが持つスイッチ名配列のインデックスを基準とする
- 先頭は飛ばさずに使う
- セーブデータの配列範囲外の場合値はnullにする

|||
|-|-|
|Items.jsonに記されたアイテム配列|先頭null 途中にnullがあるかも？|
|セーブデータに記された所持アイテム|アイテムIDをキー、所持数を値としたオブジェクト|

- アイテム一覧からnullは除外する
- 所持アイテムはアイテム一覧からIDを探す形にする

- セーブデータのスイッチ、変数、アクターの最初の要素は必ずnull
- モデルとしては不要なのでロード時には飛ばす
- セーブ時に先頭にnullを追加して保存する
- アクターは途中の要素でもnullの可能性があるのでnullableでモデル化する

## ユースケース
- wwwフォルダを入力してセーブデータを読み込み画面に表示する
- 画面に表示されたパラメーターを編集し、セーブデータに上書きする
- セーブデータに変更があった場合画面に反映する
    - 自分で行った編集直後の変更は反映しないようにする カーソルの状態がリセットされるので

## Result
- Result型で成功か失敗かと成功時の戻り値を渡せるのはいいけど、メッセージはErrorOccurredイベントとかでもいいのかも？でもイベントの購読がめんどくさいね

## CQRS
- CQRSで書いてみたけどめんどくさいというかそぐわない気がする

## セーブ
- 保存要求が来てすぐに保存すると値の一括変更など連続した保存要求に時間がかかってしまうので保存の実行を遅延させて連続した要求の最後にまとめて保存するようにする
- 保存要求毎にロードすると画面が更新されて操作しにくいので保存直後にファイルに変更があってもロードしない

## ロード
- データ保存直後はファイル変更イベントが発生するがその変更をソフト側は知っているのでロードしないよう抑制する
- セーブに遅延を持たせた上でデータ変更要求ごとにロードすると古いデータに一部の変更のみが保存されてしまう
- モデルでロードデータを持っておいて適切に同期を取っていくことにする
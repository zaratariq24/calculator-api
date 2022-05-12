# API

```csharp
APIController aPIController = client.APIController;
```

## Class Name

`APIController`


# Address

```csharp
AddressAsync(
    string appartmentno,
    int streetno)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `appartmentno` | `string` | Query, Required | **Constraints**: *Minimum Length*: `5`, *Maximum Length*: `20` |
| `streetno` | `int` | Query, Required | **Constraints**: `>= 5`, `<= 100`, *Multiple Of*: `5`, *Total Digits*: `3` |

## Response Type

`Task`

## Example Usage

```csharp
string appartmentno = "Appartmentno2";
int streetno = 246;

try
{
    await aPIController.AddressAsync(appartmentno, streetno);
}
catch (ApiException e){};
```


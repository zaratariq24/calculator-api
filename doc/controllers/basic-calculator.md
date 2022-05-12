# Basic Calculator

```csharp
BasicCalculatorController basicCalculatorController = client.BasicCalculatorController;
```

## Class Name

`BasicCalculatorController`


# Calculate

```csharp
CalculateAsync(
    double x,
    double y,
    Models.OperationTypeEnum operation)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `x` | `double` | Query, Required | LHS Value |
| `y` | `double` | Query, Required | RHS Value |
| `operation` | [`Models.OperationTypeEnum`](../../doc/models/operation-type-enum.md) | Template, Required | - |

## Response Type

`Task<double>`

## Example Usage

```csharp
double x = 222.14;
double y = 165.14;
OperationTypeEnum operation = OperationTypeEnum.SUM;

try
{
    double? result = await basicCalculatorController.CalculateAsync(x, y, operation);
}
catch (ApiException e){};
```


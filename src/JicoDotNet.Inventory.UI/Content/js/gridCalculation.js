"use strict";

//function Calculation(PriceObj, ChangeKey) {
//    var Amount = isNaN(parseFloat(PriceObj.Amount)) ? 0.0 : parseFloat(PriceObj.Amount);
//    var DiscountPercentage = isNaN(parseFloat(PriceObj.DiscountPercentage)) ? 0 : parseFloat(PriceObj.DiscountPercentage);
//    var DiscountAmount = isNaN(parseFloat(PriceObj.DiscountAmount)) ? 0 : parseFloat(PriceObj.DiscountAmount);
//    var Price = isNaN(parseFloat(PriceObj.Price)) ? 0 : parseFloat(PriceObj.Price);
//    var Quantity = isNaN(parseFloat(PriceObj.Quantity)) ? 0 : parseFloat(PriceObj.Quantity);
//    var SubTotal = isNaN(parseFloat(PriceObj.SubTotal)) ? 0 : parseFloat(PriceObj.SubTotal);
//    var TaxPercentage = isNaN(parseFloat(PriceObj.TaxPercentage)) ? 0 : parseFloat(PriceObj.TaxPercentage);
//    var TaxAmount = isNaN(parseFloat(PriceObj.TaxAmount)) ? 0 : parseFloat(PriceObj.TaxAmount);
//    var Total = isNaN(parseFloat(PriceObj.Total)) ? 0 : parseFloat(PriceObj.Total);

//    switch (ChangeKey) {
//        case 'Amount':
//            {
//                DiscountAmount = (DiscountPercentage / 100) * Amount;
//                Price = Amount - DiscountAmount;
//                SubTotal = Price * Quantity;
//                TaxAmount = (TaxPercentage / 100) * SubTotal;
//                Total = SubTotal + TaxAmount;
//                break;
//            }
//        case 'DiscountPercentage':
//            {
//                DiscountAmount = (DiscountPercentage / 100) * Amount;
//                Price = Amount - DiscountAmount;
//                SubTotal = Price * Quantity;
//                TaxAmount = (TaxPercentage / 100) * SubTotal;
//                Total = SubTotal + TaxAmount;
//                break;
//            }
//        case 'DiscountAmount':
//            {
//                DiscountPercentage = (DiscountAmount / Amount) * 100;
//                Price = Amount - DiscountAmount;
//                SubTotal = Price * Quantity;
//                TaxAmount = (TaxPercentage / 100) * SubTotal;
//                Total = SubTotal + TaxAmount;
//                break;
//            }
//        case 'Price':
//            {
//                DiscountPercentage = ((Amount - Price) / Amount) * 100;
//                DiscountAmount = (DiscountPercentage / 100) * Amount;
//                SubTotal = Price * Quantity;
//                TaxAmount = (TaxPercentage / 100) * SubTotal;
//                Total = SubTotal + TaxAmount;
//                break;
//            }
//        case 'Quantity':
//            {
//                SubTotal = Price * Quantity;
//                TaxAmount = (TaxPercentage / 100) * SubTotal;
//                Total = SubTotal + TaxAmount;
//                break;
//            }
//        case 'SubTotal':
//            {
//                Price = SubTotal / Quantity;
//                DiscountPercentage = ((Amount - Price) / Amount) * 100;
//                DiscountAmount = (DiscountPercentage / 100) * Amount;
//                TaxAmount = (TaxPercentage / 100) * SubTotal;
//                Total = SubTotal + TaxAmount;
//                break;
//            }
//        case 'TaxPercentage':
//            {
//                break;
//            }
//        case 'TaxAmount':
//            {
//                break;
//            }
//        case 'Total':
//            {
//                SubTotal = (Total * 100) / (100 + TaxPercentage);
//                TaxAmount = Total - SubTotal;
//                Price = SubTotal / Quantity;
//                DiscountPercentage = ((Amount - Price) / Amount) * 100;
//                DiscountAmount = (DiscountPercentage / 100) * Amount;
//                break;
//            }
//        default:
//            {
//                Amount = 0.0;
//                DiscountPercentage = 0.0;
//                DiscountAmount = 0.0;
//                Price = 0.0;
//                Quantity = 0.0;
//                SubTotal = 0.0;
//                TaxPercentage = 0.0;
//                TaxAmount = 0.0;
//                Total = 0.0;
//            }
//    }
//    PriceObj.Amount = Amount;
//    PriceObj.DiscountPercentage = isNaN(DiscountPercentage) ? 0 : isFinite(DiscountPercentage) ? DiscountPercentage : 0;
//    PriceObj.DiscountAmount = isNaN(DiscountAmount) ? 0 : DiscountAmount;
//    PriceObj.Price = Price;
//    PriceObj.Quantity = Quantity;
//    PriceObj.SubTotal = SubTotal;
//    PriceObj.TaxPercentage = TaxPercentage;
//    PriceObj.TaxAmount = TaxAmount;
//    PriceObj.Total = Total;
//    return PriceObj;
//}

function Calculation(a, t) { var e = isNaN(parseFloat(a.Amount)) ? 0 : parseFloat(a.Amount), o = isNaN(parseFloat(a.DiscountPercentage)) ? 0 : parseFloat(a.DiscountPercentage), s = isNaN(parseFloat(a.DiscountAmount)) ? 0 : parseFloat(a.DiscountAmount), r = isNaN(parseFloat(a.Price)) ? 0 : parseFloat(a.Price), n = isNaN(parseFloat(a.Quantity)) ? 0 : parseFloat(a.Quantity), i = isNaN(parseFloat(a.SubTotal)) ? 0 : parseFloat(a.SubTotal), c = isNaN(parseFloat(a.TaxPercentage)) ? 0 : parseFloat(a.TaxPercentage), u = isNaN(parseFloat(a.TaxAmount)) ? 0 : parseFloat(a.TaxAmount), l = isNaN(parseFloat(a.Total)) ? 0 : parseFloat(a.Total); switch (t) { case "Amount": s = o / 100 * e, r = e - s, i = r * n, u = c / 100 * i, l = i + u; break; case "DiscountPercentage": s = o / 100 * e, r = e - s, i = r * n, u = c / 100 * i, l = i + u; break; case "DiscountAmount": o = s / e * 100, r = e - s, i = r * n, u = c / 100 * i, l = i + u; break; case "Price": o = (e - r) / e * 100, s = o / 100 * e, i = r * n, u = c / 100 * i, l = i + u; break; case "Quantity": i = r * n, u = c / 100 * i, l = i + u; break; case "SubTotal": r = i / n, o = (e - r) / e * 100, s = o / 100 * e, u = c / 100 * i, l = i + u; break; case "TaxPercentage": break; case "TaxAmount": break; case "Total": i = 100 * l / (100 + c), u = l - i, r = i / n, o = (e - r) / e * 100, s = o / 100 * e; break; default: e = 0, o = 0, s = 0, r = 0, n = 0, i = 0, c = 0, u = 0, l = 0; }return a.Amount = e, a.DiscountPercentage = isNaN(o) ? 0 : isFinite(o) ? o : 0, a.DiscountAmount = isNaN(s) ? 0 : s, a.Price = r, a.Quantity = n, a.SubTotal = i, a.TaxPercentage = c, a.TaxAmount = u, a.Total = l, a; }
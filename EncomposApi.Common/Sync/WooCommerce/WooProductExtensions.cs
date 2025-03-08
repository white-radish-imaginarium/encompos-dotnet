using EncomposApi.Models;
using EncomposApi.Types.Optional;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncomposApi.Sync.WooCommerce;

public static class WooProductExtensions
{
    public static WooProductModel CopyFrom(this WooProductModel product, InventoryModel inventory, PromotionItemModel promotionItem)
    {
        product.Sku = inventory.ProductCode;
        inventory.Description.WhenPresent(value =>
        {
            product.Name = value;
        });

        inventory.RetailPrice.WhenPresent(value => product.RegularPrice = $"{value:N2}");

        if (promotionItem != null)
        {
            var definitionId = promotionItem.DefinitionId.ValueOr(0);
            var newPrice = promotionItem.NewPrice.ValueOr(0);
            if (definitionId == 3)
            {
                product.SalePrice = $"{newPrice:N2}";
            }
            else
            {
                product.SalePrice = null;
            }
        }

        return product;
    }

    public static WooProductModel ToUpdateModel(this WooProductModel product)
    {
        return new WooProductModel
        {
            Name = product.Name,
            RegularPrice = product.RegularPrice,
            Sku = product.Sku,
        };
    }

    public static WooProductModel ToCreateModel(this WooProductModel product)
    {
        return new WooProductModel
        {
            Name = product.Name,
            RegularPrice = product.RegularPrice,
            Sku = product.Sku,
        };
    }
}

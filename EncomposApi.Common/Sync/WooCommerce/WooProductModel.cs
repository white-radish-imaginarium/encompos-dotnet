using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EncomposApi.Types.Optional;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EncomposApi.Sync.WooCommerce
{
    public class WooProductModel
    {
        public Optional<int> Id { get; set; }
        /// <summary>
        /// Product name.
        /// </summary>
        public Optional<string> Name { get; set; }

        /// <summary>
        /// Product slug.
        /// </summary>
        public Optional<string> Slug { get; set; }

        /// <summary>
        /// Product URL. READ-ONLY
        /// </summary>
        public Optional<string> Permalink { get; set; }

        /// <summary>
        /// The date the product was created, in the site's timezone. READ-ONLY
        /// </summary>
        public Optional<DateTime> DateCreated { get; set; }

        /// <summary>
        /// The date the product was created, as GMT. READ-ONLY
        /// </summary>
        public Optional<DateTime> DateCreatedGmt { get; set; }

        /// <summary>
        /// The date the product was last modified, in the site's timezone. READ-ONLY
        /// </summary>
        public Optional<DateTime> DateModified { get; set; }

        /// <summary>
        /// The date the product was last modified, as GMT. READ-ONLY
        /// </summary>
        public Optional<DateTime> DateModifiedGmt { get; set; }

        /// <summary>
        /// Product type. Options: simple, grouped, external and variable. Default is simple.
        /// </summary>
        public Optional<WooProductType> Type { get; set; }

        /// <summary>
        /// Product status (post status). Options: draft, pending, private and publish. Default is publish.
        /// </summary>
        public Optional<WooProductStatus> Status { get; set; }

        /// <summary>
        /// Featured product. Default is false.
        /// </summary>
        public Optional<bool> Featured { get; set; }

        /// <summary>
        /// Catalog visibility. Options: visible, catalog, search and hidden. Default is visible.
        /// </summary>
        public Optional<string> CatalogVisibility { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public Optional<string> Description { get; set; }

        /// <summary>
        /// Product short description.
        /// </summary>
        public Optional<string> ShortDescription { get; set; }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public Optional<string> Sku { get; set; }

        /// <summary>
        /// Current product price. READ-ONLY
        /// </summary>
        public Optional<string> Price { get; set; }

        /// <summary>
        /// Product regular price.
        /// </summary>
        public Optional<string> RegularPrice { get; set; }

        /// <summary>
        /// Product sale price.
        /// </summary>
        public Optional<string> SalePrice { get; set; }

        /// <summary>
        /// Start date of sale price, in the site's timezone.
        /// </summary>
        public Optional<DateTime> DateOnSaleFrom { get; set; }

        /// <summary>
        /// Start date of sale price, as GMT.
        /// </summary>
        public Optional<DateTime> DateOnSaleFromGmt { get; set; }

        /// <summary>
        /// End date of sale price, in the site's timezone.
        /// </summary>
        public Optional<DateTime> DateOnSaleTo { get; set; }

        /// <summary>
        /// End date of sale price, as GMT.
        /// </summary>
        public Optional<DateTime> DateOnSaleToGmt { get; set; }

        /// <summary>
        /// Price formatted in HTML. READ-ONLY
        /// </summary>
        public Optional<string> PriceHtml { get; set; }

        /// <summary>
        /// Shows if the product is on sale. READ-ONLY
        /// </summary>
        public Optional<bool> OnSale { get; set; }

        /// <summary>
        /// Shows if the product can be bought. READ-ONLY
        /// </summary>
        public Optional<bool> Purchasable { get; set; }

        /// <summary>
        /// Amount of sales. READ-ONLY
        /// </summary>
        public Optional<int> TotalSales { get; set; }

        /// <summary>
        /// If the product is virtual. Default is false.
        /// </summary>
        public Optional<bool> Virtual { get; set; }

        /// <summary>
        /// If the product is downloadable. Default is false.
        /// </summary>
        public Optional<bool> Downloadable { get; set; }

        /// <summary>
        /// List of downloadable files. See Product - Downloads properties
        /// </summary>
        public Optional<List<Download>> Downloads { get; set; }

        /// <summary>
        /// Number of times downloadable files can be downloaded after purchase. Default is -1.
        /// </summary>
        public Optional<int> DownloadLimit { get; set; }

        /// <summary>
        /// Number of days until access to downloadable files expires. Default is -1.
        /// </summary>
        public Optional<int> DownloadExpiry { get; set; }

        /// <summary>
        /// Product external URL. Only for external products.
        /// </summary>
        public Optional<string> ExternalUrl { get; set; }

        /// <summary>
        /// Product external button text. Only for external products.
        /// </summary>
        public Optional<string> ButtonText { get; set; }

        /// <summary>
        /// Tax status. Options: taxable, shipping and none. Default is taxable.
        /// </summary>
        public Optional<string> TaxStatus { get; set; }

        /// <summary>
        /// Tax class.
        /// </summary>
        public Optional<string> TaxClass { get; set; }

        /// <summary>
        /// Stock management at product level. Default is false.
        /// </summary>
        public Optional<bool> ManageStock { get; set; }

        /// <summary>
        /// Stock quantity.
        /// </summary>
        public Optional<int> StockQuantity { get; set; }

        /// <summary>
        /// Controls the stock status of the product. Options: instock, outofstock, onbackorder. Default is instock.
        /// </summary>
        public Optional<string> StockStatus { get; set; }

        /// <summary>
        /// If managing stock, this controls if backorders are allowed. Options: no, notify and yes. Default is no.
        /// </summary>
        public Optional<string> Backorders { get; set; }

        /// <summary>
        /// Shows if backorders are allowed. READ-ONLY
        /// </summary>
        public Optional<bool> BackordersAllowed { get; set; }

        /// <summary>
        /// Shows if the product is on backordered. READ-ONLY
        /// </summary>
        public Optional<bool> Backordered { get; set; }

        /// <summary>
        /// Allow one item to be bought in a single order. Default is false.
        /// </summary>
        public Optional<bool> SoldIndividually { get; set; }

        /// <summary>
        /// Product weight.
        /// </summary>
        public Optional<string> Weight { get; set; }

        /// <summary>
        /// Product dimensions. See Product - Dimensions properties
        /// </summary>
        public Optional<DimensionsModel> Dimensions { get; set; }

        /// <summary>
        /// Shows if the product need to be shipped. READ-ONLY
        /// </summary>
        public Optional<bool> ShippingRequired { get; set; }

        /// <summary>
        /// Shows whether or not the product shipping is taxable. READ-ONLY
        /// </summary>
        public Optional<bool> ShippingTaxable { get; set; }

        /// <summary>
        /// Shipping class slug.
        /// </summary>
        public Optional<string> ShippingClass { get; set; }

        /// <summary>
        /// Shipping class ID. READ-ONLY
        /// </summary>
        public Optional<int> ShippingClassId { get; set; }

        /// <summary>
        /// Allow reviews. Default is true.
        /// </summary>
        public Optional<bool> ReviewsAllowed { get; set; }

        /// <summary>
        /// Reviews average rating. READ-ONLY
        /// </summary>
        public Optional<string> AverageRating { get; set; }

        /// <summary>
        /// Amount of reviews that the product have. READ-ONLY
        /// </summary>
        public Optional<int> RatingCount { get; set; }

        /// <summary>
        /// List of related products IDs. READ-ONLY
        /// </summary>
        public Optional<List<int>> RelatedIds { get; set; }

        /// <summary>
        /// List of up-sell products IDs.
        /// </summary>
        public Optional<List<int>> UpsellIds { get; set; }

        /// <summary>
        /// List of cross-sell products IDs.
        /// </summary>
        public Optional<List<int>> CrossSellIds { get; set; }

        /// <summary>
        /// Product parent ID.
        /// </summary>
        public Optional<int> ParentId { get; set; }

        /// <summary>
        /// Optional note to send the customer after purchase.
        /// </summary>
        public Optional<string> PurchaseNote { get; set; }

        /// <summary>
        /// List of categories. See Product - Categories properties
        /// </summary>
        public Optional<List<Category>> Categories { get; set; }

        /// <summary>
        /// List of tags. See Product - Tags properties
        /// </summary>
        public Optional<List<Tag>> Tags { get; set; }

        /// <summary>
        /// List of images. See Product - Images properties
        /// </summary>
        public Optional<List<Image>> Images { get; set; }

        /// <summary>
        /// List of attributes. See Product - Attributes properties
        /// </summary>
        public Optional<List<Attribute>> Attributes { get; set; }

        /// <summary>
        /// Defaults variation attributes. See Product - Default attributes properties
        /// </summary>
        public Optional<List<DefaultAttribute>> DefaultAttributes { get; set; }

        /// <summary>
        /// List of variations IDs. READ-ONLY
        /// </summary>
        public Optional<List<int>> Variations { get; set; }

        /// <summary>
        /// List of grouped products ID.
        /// </summary>
        public Optional<List<int>> GroupedProducts { get; set; }

        /// <summary>
        /// Menu order, used to custom sort products.
        /// </summary>
        public Optional<int> MenuOrder { get; set; }

        /// <summary>
        /// Meta data. See Product - Meta data properties
        /// </summary>
        public Optional<List<MetaDatum>> MetaData { get; set; }

        /// <summary>
        /// Represents an attribute associated with a product.
        /// </summary>
        public class Attribute
        {
            /// <summary>
            /// Gets or sets the attribute ID.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the attribute name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the attribute position.
            /// </summary>
            public int Position { get; set; }

            /// <summary>
            /// Gets or sets whether the attribute is visible on the "Additional information" tab in the product's page. Default is false.
            /// </summary>
            public bool Visible { get; set; }

            /// <summary>
            /// Gets or sets whether the attribute can be used as a variation. Default is false.
            /// </summary>
            public bool Variation { get; set; }

            /// <summary>
            /// Gets or sets the list of available term names of the attribute.
            /// </summary>
            public List<string> Options { get; set; }
        }

        /// <summary>
        /// Represents a product category.
        /// </summary>
        public class Category
        {
            /// <summary>
            /// Gets or sets the category ID.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the category name. READ-ONLY
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the category slug. READ-ONLY
            /// </summary>
            public string Slug { get; set; }
        }

        /// <summary>
        /// Represents a default attribute associated with a product variation.
        /// </summary>
        public class DefaultAttribute
        {
            /// <summary>
            /// Gets or sets the attribute ID.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the attribute name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the selected attribute term name.
            /// </summary>
            public string Option { get; set; }
        }

        /// <summary>
        /// Represents the dimensions of a product.
        /// </summary>
        public class DimensionsModel
        {
            /// <summary>
            /// Gets or sets the product length.
            /// </summary>
            public string Length { get; set; }

            /// <summary>
            /// Gets or sets the product width.
            /// </summary>
            public string Width { get; set; }

            /// <summary>
            /// Gets or sets the product height.
            /// </summary>
            public string Height { get; set; }
        }

        /// <summary>
        /// Represents a downloadable file associated with a product.
        /// </summary>
        public class Download
        {
            /// <summary>
            /// Gets or sets the file ID.
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the file name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the file URL.
            /// </summary>
            public string File { get; set; }
        }

        /// <summary>
        /// Represents an image associated with a product.
        /// </summary>
        public class Image
        {
            /// <summary>
            /// Gets or sets the image ID.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the date the image was created, in the site's timezone. READ-ONLY
            /// </summary>
            public DateTime DateCreated { get; set; }

            /// <summary>
            /// Gets or sets the date the image was created, as GMT. READ-ONLY
            /// </summary>
            public DateTime DateCreatedGmt { get; set; }

            /// <summary>
            /// Gets or sets the date the image was last modified, in the site's timezone. READ-ONLY
            /// </summary>
            public DateTime DateModified { get; set; }

            /// <summary>
            /// Gets or sets the date the image was last modified, as GMT. READ-ONLY
            /// </summary>
            public DateTime DateModifiedGmt { get; set; }

            /// <summary>
            /// Gets or sets the image URL.
            /// </summary>
            public string Src { get; set; }

            /// <summary>
            /// Gets or sets the image name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the image alternative text.
            /// </summary>
            public string Alt { get; set; }
        }

        /// <summary>
        /// Represents meta data associated with a product.
        /// </summary>
        public class MetaDatum
        {
            /// <summary>
            /// Gets or sets the meta ID. READ-ONLY
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the meta key.
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// Gets or sets the meta value.
            /// </summary>
            public JToken Value { get; set; }
        }

        /// <summary>
        /// Represents a product tag.
        /// </summary>
        public class Tag
        {
            /// <summary>
            /// Gets or sets the tag ID.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the tag name. READ-ONLY
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the tag slug. READ-ONLY
            /// </summary>
            public string Slug { get; set; }
        }
    }
}
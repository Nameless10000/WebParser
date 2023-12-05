import '@umijs/max/typings';

export type BestOffer = {
    name: string;
    bestPrice: number;
    url: string;
}

export type Shop = {
    name: string;
    shopId: number;
}

export type Name = {
    name: string;
    productsNamesId: number;
}

export type Product = {
    productId: number;
    url: string;
    lastFetchedPrice: number;
    nameId: number;
    name: Name;
    shopId: number;
    shop: Shop;
}
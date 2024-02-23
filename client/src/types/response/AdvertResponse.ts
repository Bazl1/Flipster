export interface Advert {
    id: string;
    title: string;
    description: string;
    images: string[];
    category: {
        id: number;
        title: string;
        icon?: string;
    };
    productType: string;
    businessType: string;
    createAt: string;
    isFree: boolean;
    price: string;
    contact: {
        id: string;
        name?: string;
        avatar?: string;
        location?: string;
        email: string;
        phoneNumber?: string;
    };
}

export interface AdvertResponse {
    pageCount: number;
    adverts: Advert[];
}

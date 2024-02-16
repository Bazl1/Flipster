export interface IAdvert {
    id: string;
    sellerId: string;
    email: string;
    phoneNumber: string;
    title: string;
    description: string;
    isFree: boolean;
    price: number;
    productType: string;
    businessType: string;
    location: string;
    createAt: string;
    categoryId: number;
    images: string[] | FormData;
}

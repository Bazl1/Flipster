import { useToggleAddFavoriteMutation } from "../../services/FavoriteAdvertService";

interface IFavorite {
    id: string;
}

const useFavorite = (id: string) => {
    const [toggleAddFavorite] = useToggleAddFavoriteMutation();

    const toggleFavorite = async () => {
        let favorite: IFavorite[] = JSON.parse(localStorage.getItem("favorite") || "[]");
        const isExists = favorite.some((item: IFavorite) => item.id === id);

        if (isExists) {
            favorite = favorite.filter((item) => item.id !== id);
        } else {
            favorite.push({ id: id });
        }

        await toggleAddFavorite({ advertId: id });
        localStorage.setItem("favorite", JSON.stringify(favorite));
    };

    return toggleFavorite;
};

export default useFavorite;

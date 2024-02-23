interface IFavorite {
    id: string;
}

const useFavorite = async (id: string) => {
    let favorite: IFavorite[] = JSON.parse(localStorage.getItem("favorite") || "[]");
    const isExists = favorite.some((item: IFavorite) => item.id === id);

    if (isExists) {
        favorite = favorite.filter((item) => item.id !== id);
    } else {
        favorite.push({ id: id });
    }

    localStorage.setItem("favorite", JSON.stringify(favorite));
};

export default useFavorite;

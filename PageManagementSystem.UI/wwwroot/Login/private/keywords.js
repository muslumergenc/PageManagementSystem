document.addEventListener("DOMContentLoaded", function () {
    const seoKeywordsInput = document.getElementById("seoKeywordsInput");
    const seoKeywordsContainer = document.getElementById("seoKeywordsContainer");
    const seoKeywordsHiddenInput = document.getElementById("SeoKeywords");

    let keywords = [];

    seoKeywordsInput.addEventListener("keyup", function (event) {
        if (event.key === "," || event.key === "Enter") {
            event.preventDefault();
            addKeyword(seoKeywordsInput.value.trim());
            seoKeywordsInput.value = "";
        }
    });

    function addKeyword(keyword) {
        // Anahtar kelimeden gereksiz virgülleri ve boşlukları temizle
        keyword = keyword.replace(/,+$/, "").trim();

        if (keyword && !keywords.includes(keyword)) {
            keywords.push(keyword); // Listeye ekle
            updateHiddenInput();
            renderChips(); // Chip'leri yeniden render et
        }
    }

    function removeKeyword(keyword) {
        // Seçilen anahtar kelimeyi listeden kaldır
        keywords = keywords.filter(kw => kw !== keyword);
        updateHiddenInput();
        renderChips(); // Güncellenen listeyi göster
    }

    function updateHiddenInput() {
        // keywords listesini virgülle ayrılmış bir şekilde gizli inputa kaydet
        seoKeywordsHiddenInput.value = keywords.join(", ");
    }

    function renderChips() {
        // Var olan tüm chip'leri silip güncellenmiş listeyi ekler
        seoKeywordsContainer.querySelectorAll(".tag-chip").forEach(chip => chip.remove());

        keywords.forEach(keyword => {
            const chip = document.createElement("span");
            chip.classList.add("tag-chip");
            chip.textContent = keyword;

            const closeBtn = document.createElement("span");
            closeBtn.classList.add("close-btn");
            closeBtn.textContent = "×";
            closeBtn.onclick = function () {
                removeKeyword(keyword);
            };

            chip.appendChild(closeBtn);
            seoKeywordsContainer.insertBefore(chip, seoKeywordsInput);
        });
    }

    // Sayfa yüklendiğinde önceki anahtar kelimeleri yükleme
    const initialKeywords = seoKeywordsHiddenInput.value.split(",").map(kw => kw.trim()).filter(kw => kw);
    initialKeywords.forEach(addKeyword);
});
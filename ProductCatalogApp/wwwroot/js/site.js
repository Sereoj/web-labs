window.downloadFile = (filename, contentType, content) => {
    // Преобразуем байты в объект Blob
    const blob = new Blob([new Uint8Array(content)], { type: contentType });
    
    // Создаем URL для скачивания
    const url = window.URL.createObjectURL(blob);
    
    // Создаем элемент ссылки для скачивания
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    
    // Добавляем элемент во временный div и кликаем
    document.body.appendChild(a);
    a.click();
    
    // Удаляем элемент и освобождаем URL
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
};
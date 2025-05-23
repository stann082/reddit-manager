window.highlightText = (element, terms, caseSensitive = false) => {
    if (!terms || !Array.isArray(terms) || terms.length === 0 || !element) return;

    // Clear previous highlights
    const unmark = el => {
        const marks = el.querySelectorAll('mark');
        marks.forEach(mark => {
            const textNode = document.createTextNode(mark.textContent);
            mark.replaceWith(textNode);
        });
    };
    unmark(element);

    const walk = (node) => {
        if (node.nodeType === Node.TEXT_NODE) {
            let text = node.textContent;
            let modified = false;

            for (let term of terms) {
                if (!term) continue;

                const flags = caseSensitive ? 'g' : 'gi';
                const regex = new RegExp(term.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'), flags); // escape regex

                if (regex.test(text)) {
                    text = text.replace(regex, match => `<mark>${match}</mark>`);
                    modified = true;
                }
            }

            if (modified) {
                const span = document.createElement('span');
                span.innerHTML = text;
                node.replaceWith(span);
            }
        } else {
            node.childNodes.forEach(walk);
        }
    };

    walk(element);
};

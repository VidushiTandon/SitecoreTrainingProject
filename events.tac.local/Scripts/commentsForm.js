function createCommentItem(form, path) {
    var service = new ItemService({ url: '/sitecore/api/ssc/item' });
    var obj = {
        ItemName: 'comment - ' + form.name.value,
        TemplateID: '{A3B79338-B0CC-4DB5-9E47-C06C079FC79F}',
        Name: form.name.value,
        Comment: form.comment.value
    };
    service.create(obj)
        .path(path)
        .execute()
        .then(function (item) {
            form.name.value = form.comment.value = '';
            window.alert('Thanks. Your message will show in site shortly');
        })
        .fail(function (err) { window.alert(err); });
    event.preventDefault();
    return false;
    }

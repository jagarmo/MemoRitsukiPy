from django.contrib import admin
from cms.models import Memo, Sentence, User

# Register your models here.
class MemoAdmin(admin.ModelAdmin):
    list_display = ('id','name','date','author',)
    list_display_links = ('id','name',)

admin.site.register(Memo,MemoAdmin)


class SentenceAdmin(admin.ModelAdmin):
    list_display = ('id','content',)
    list_display_links = ('id','content',)
    #raw_id_fields = ('memo',)


admin.site.register(Sentence,SentenceAdmin)

class UserAdmin(admin.ModelAdmin):
    list_display = ('id','username',)
    list_display_links = ('id','username',)
    #raw_id_fields = ('memo',)


admin.site.register(User,UserAdmin)
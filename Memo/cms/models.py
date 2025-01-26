from django.db import models

# Create your models here.
class Memo(models.Model):
    """メモ"""
    name = models.CharField('タイトル',max_length=255)
    date = models.DateTimeField('更新日時',auto_now=True)
    author = models.CharField('作成者',max_length=255,default='ritsuki')

    def __str__(self):
        return self.name
    

class Sentence(models.Model):
    """文章"""
    #memo = models.ForeignKey(Memo, verbose_name='タイトル', related_name='sentence', on_delete=models.CASCADE)
    content = models.TextField('内容',blank=True)

    def __str__(self):
        return self.content
    
class User(models.Model):
    """ユーザー"""
    username = models.CharField('ユーザーネーム',max_length=255)
    password = models.CharField('パスワード',max_length=255)

    def __str__(self):
        return self.username
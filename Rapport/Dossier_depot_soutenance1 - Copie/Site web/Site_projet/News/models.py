from django.db import models
from django.utils import timezone

class Article(models.Model):
    titre = models.CharField(max_length=100)
    texte = models.TextField()
    date = models.DateTimeField(default=timezone.now, 
                                verbose_name="Date de parution")
    
    def __str__(self):
        return self.titre
